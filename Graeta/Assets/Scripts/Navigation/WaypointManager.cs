using UnityEngine;

namespace Navigation
{
    [ExecuteInEditMode]
    public class WaypointManager : MonoBehaviour
    {
        public Waypoint[] waypoints;

        public void OnEnable()
        {
            waypoints = gameObject.GetComponentsInChildren<Waypoint>();
        }

        void OnDrawGizmos()
        {
            
#if UNITY_EDITOR
            waypoints = gameObject.GetComponentsInChildren<Waypoint>();
            for (int i = 0; i < waypoints.Length; i++)
            {
                Waypoint current = waypoints[i];
                if (current.connections == null)
                {
                    continue;
                }
                
                for (int j = 0; j < current.connections.Count; j++)
                {
                    if (current.connections[j].pointIngTo == null)
                    {
                        Debug.LogError("Please set pointing to");
                    }
                    Waypoint finish = current.connections[j].pointIngTo;
                    DrawLineBetweenWaypoints(current, finish);
                }
            }
            
#endif
        }

        private void DrawLineBetweenWaypoints(Waypoint start, Waypoint finish)
        {
            Gizmos.color = Color.red;
            
            //Draw the suspension
            Gizmos.DrawLine(
                start.transform.position,
                finish.transform.position
            );

            Gizmos.color = Color.white;
        }

        public Waypoint GetNearestWaypoint(Vector2 positon)
        {
            if (waypoints == null || waypoints.Length == 0)
            {
                return null;
            }
            
            Waypoint nearest = waypoints[0];
            float distance = (nearest.position - positon).magnitude;
            
            for (int i = 1; i < waypoints.Length; i++)
            {
                float newDistance = (waypoints[i].position - positon).magnitude;
                if ( newDistance < distance)
                {
                    distance = newDistance;
                    nearest = waypoints[i];
                }
            }

            return nearest;
        }
    }
}
