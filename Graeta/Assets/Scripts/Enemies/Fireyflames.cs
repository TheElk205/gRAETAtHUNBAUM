using Navigation;
using UnityEngine;

namespace Enemies
{
    public class Fireyflames : MoveWithWaypoints
    {
        public float spawnDelay = 0.2f;
        public float decayTime;
        public GameObject flameling;
        int counter;
        float timer;

        new public void Start()
        {
            base.Start();
            Transform toMove = transform.Find("Body");
            if (toMove != null)
            {
                toMove.localPosition = new Vector3(0,0,0);
            }
        }
        // Update is called once per frame
        new void Update()
        {
            base.Update();
            SpawnFlame();
            
        }
        
        void SpawnFlame()
        {
            timer += Time.deltaTime;

            if (timer > spawnDelay)
            {
                timer = 0;
                GameObject temp = Instantiate(flameling, transform.parent);
                temp.transform.position = transform.position;
            }
        }
    }
}
