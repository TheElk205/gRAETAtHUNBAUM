using SceneTransition;
using UnityEngine;

namespace Portal
{
    public class Portal : MonoBehaviour
    {
        public Vector2 position;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Player"))
            {
                SceneManager.sceneManager.LoadScenePositionPlayer(position);
            }
        }
    }
}
