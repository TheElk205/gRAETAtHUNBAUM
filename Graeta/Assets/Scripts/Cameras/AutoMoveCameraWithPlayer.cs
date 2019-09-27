using System;
using UnityEngine;

namespace Cameras
{
    public class AutoMoveCameraWithPlayer : MonoBehaviour
    {
        public Camera toMove;
        public Vector2 oldPlayerPos;
        public Vector3 newCameraPos;

        public Vector3 currentVelocity;
        public Transform playerTransform;

        public bool repositionCamera;

        private String playerTag = "Player";

        public float smoothPosition = 0.125f;

        public void Start()
        {
            newCameraPos = toMove.transform.position;
        }
        
        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(playerTag))
            {
                repositionCamera = true;
            }
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(playerTag))
            {
                repositionCamera = false;
            }
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(playerTag))
            {
                playerTransform = other.transform;
                oldPlayerPos = playerTransform.position;
            }
        }

        public void LateUpdate()
        {
            if (playerTransform == null || !repositionCamera)
            {
                return;
            }
            Vector2 moveBy = (Vector2) playerTransform.transform.position - oldPlayerPos;
            Debug.Log("Move by: " +  moveBy.ToString("F4"));
            newCameraPos = newCameraPos + new Vector3(moveBy.x, moveBy.y, 0);
            var transform1 = toMove.transform;
            transform1.position = newCameraPos;
        }
    }
}
