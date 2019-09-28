using UnityEngine;

namespace Player
{
    public class HidingInTreeGroup : MonoBehaviour
    {
        public bool isPlayerInTreeGroup;

        void Start()
        {
            isPlayerInTreeGroup = false;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("TreeGroup"))
            {
                isPlayerInTreeGroup = true;
            }
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("TreeGroup"))
            {
                isPlayerInTreeGroup = false;
            }
        }
    }
}
