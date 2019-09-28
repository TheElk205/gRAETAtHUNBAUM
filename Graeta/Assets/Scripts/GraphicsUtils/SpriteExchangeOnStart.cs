using UnityEngine;

namespace GraphicsUtils
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteExchangeOnStart : MonoBehaviour
    {
        public Sprite[] sprites;

        public void Start()
        {
            if (sprites == null || sprites.Length == 0)
            {
                return;
            }

            int index = Random.Range(0, sprites.Length);
            GetComponent<SpriteRenderer>().sprite = sprites[index];
        }
    }
}
