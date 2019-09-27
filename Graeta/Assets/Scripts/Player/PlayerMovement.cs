using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public float maxSpeed = 1;
        public Vector2 currentSpeed;

        [Header("Will be filled during simulation")]
        public Rigidbody2D rigidbody2D;

        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");   
            
            currentSpeed = new Vector2(h, v);

            currentSpeed = Time.deltaTime * maxSpeed * currentSpeed.normalized;
            rigidbody2D.MovePosition(rigidbody2D.transform.position + new Vector3(currentSpeed.x, currentSpeed.y, 0));
        }
    }
}
