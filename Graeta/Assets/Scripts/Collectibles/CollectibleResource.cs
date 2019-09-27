using UnityEngine;

namespace Collectibles
{
    public class CollectibleResource : MonoBehaviour
    {
        public float resources = 1.0f;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CollectiblesManager.instance.Collected(this);
            }
        }
    }
}
