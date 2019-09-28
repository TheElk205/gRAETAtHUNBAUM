using UnityEngine;

namespace Navigation
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class MoveWithWaypoints : MonoBehaviour
    {
        public WaypointManager waypointManager;

        public Vector2 startingPoint;
        public Vector2 desiredPoint;
        public float speed = 1.0f;
        public float startTime;
        
        public bool shouldAutoMove;

        private Rigidbody2D rigidbody2D;
        private Waypoint currentWaypoint;
        private Waypoint previousWaypoint;

        public float threshold = 0.01f;
        public bool constantSpeed = true;
        
        public void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            StartMovingAlongWaypoints();
        }

        public void Update()
        {
            if (!shouldAutoMove)
            {
                return;
            }

            MoveTowardsAim();
        }

        public void StopMoving()
        {
            shouldAutoMove = false;
        }
        
        public void SetDesiredWaypointAndStartMoving(Waypoint desired)
        {
            previousWaypoint = currentWaypoint;
            currentWaypoint = desired;
            
            desiredPoint = desired.position;
            startingPoint = transform.position;
            startTime = Time.time;
            shouldAutoMove = true;
        }

        private void MoveTowardsAim()
        {
            var position = transform.position;
            float distCovered = (Time.time - startTime) * speed;
            
            Vector2 newPosition = Vector2.zero;
            if (constantSpeed)
            {
                newPosition = Vector2.MoveTowards(position, desiredPoint, distCovered);
            }
            else
            {
                newPosition = Vector2.Lerp(position, desiredPoint, distCovered);
            }
            rigidbody2D.MovePosition(newPosition);
        }

        private void LateUpdate()
        {
            if (((Vector2) transform.position - desiredPoint).magnitude <= threshold)
            {
                // Waypoint reached
                Waypoint nextWaypoint = currentWaypoint.GetRandomNextWaypoint();
                if (nextWaypoint == null)
                {
                    nextWaypoint = previousWaypoint;
                    if (nextWaypoint == null)
                    {
                        nextWaypoint = waypointManager.waypoints[0];
                    }
                }

                SetDesiredWaypointAndStartMoving(nextWaypoint);
            }
        }

        public void StartMovingAlongWaypoints()
        {
            Waypoint nearestWaypoint = waypointManager.GetNearestWaypoint(transform.position);
            Debug.Log("Moving towards first point");
            SetDesiredWaypointAndStartMoving(nearestWaypoint);
        }
    }
}
