using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Navigation
{
    public class Waypoint : MonoBehaviour
    {
        public Vector2 position
        {
            get { return transform.position; }
        }
        
        public List<WaypointConnection> connections;

        public void Start()
        {
            #if !UNITY_EDITOR
            GetComponent<SpriteRenderer>().enabled = false;
            #endif
        }
        
        public Waypoint GetRandomNextWaypoint()
        {
            if (connections == null || connections.Count == 0)
            {
                return null;
            }
            
            List<float> cdfSimple = new List<float>();
            
            float overallWeight = 0;
            
            for (int i = 0; i < connections.Count; i++)
            {
                overallWeight += connections[i].weight;
                cdfSimple.Add(overallWeight);

                Debug.Log("CDF[" + i + "]: " + overallWeight);
            }
            
            Debug.Log("OverallWeight: " + overallWeight);
            float random = Random.Range(0, overallWeight);
            Debug.Log("Random: " + random);
            for (int i = 0; i < cdfSimple.Count; i++)
            {
                if (random <= cdfSimple[i])
                {
                    Debug.Log("Selecting point " + i);
                    return connections[i].pointIngTo;
                }
            }

            Debug.Log("Nothing found :shrug:");
            return null;
        }
    }

    [Serializable]
    public class WaypointConnection
    {
        public float weight = 1.0f;
        public Waypoint pointIngTo;
    }
}