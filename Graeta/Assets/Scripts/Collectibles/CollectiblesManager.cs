using UnityEngine;

namespace Collectibles
{
    public class CollectiblesManager : MonoBehaviour
    {
        public static CollectiblesManager instance { private set; get; }

        public float collectedResources;
        
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Collected(CollectibleResource resource)
        {
            collectedResources += resource.resources;
            
            Destroy(resource.gameObject);
        }
    }
}
