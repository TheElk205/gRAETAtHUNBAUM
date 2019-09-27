using SceneTransition;
using UnityEngine;

namespace Portal
{
    public class Portal : MonoBehaviour
    {
        public SceneNames loadNext;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Player"))
            {
                SceneManager.LoadNextScene();
            }
        }
    }
}
