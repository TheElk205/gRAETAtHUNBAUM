using UnityEngine;

namespace Portal
{
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Player"))
            {
                Debug.Log("Loading Scene");
            }
        }
    }
}
