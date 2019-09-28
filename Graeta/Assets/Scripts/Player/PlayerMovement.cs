using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private float maxSpeed = 1;
        public float defaultSpeed = 1;
        public float boostedSpeed = 2;
        public Vector2 currentSpeed;

        [Header("Will be filled during simulation")]
        public Rigidbody2D rigidbody2D;

        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            #if !UNITY_EDITOR
            defaultSpeed =*= 10;
            #endif
            
            maxSpeed = defaultSpeed;
        }
        
        void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");   
            
            currentSpeed = new Vector2(h, v);

            currentSpeed = Time.deltaTime * maxSpeed * currentSpeed.normalized;
            rigidbody2D.MovePosition(rigidbody2D.transform.position + new Vector3(currentSpeed.x, currentSpeed.y, 0));
        }

        public bool isMoving()
        {
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
                return true;
            }
            return false;
        }

        public void boostSpeed()
        {
            maxSpeed = boostedSpeed;
        }

        public void resetSpeed()
        {
            maxSpeed = defaultSpeed;
        }
    }
}
