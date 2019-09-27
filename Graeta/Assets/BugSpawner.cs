using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject bug;
    public float spawnDelay;
    private float spawnTimer = 0;

    void Update()
    {
        if (transform.childCount == 0)
        {
            spawnTimer += Time.deltaTime;
            if(spawnTimer > spawnDelay)
            {
                spawnTimer = 0;
                GameObject temp = Instantiate(bug, transform);
                temp.GetComponent<bug>().home = GetComponent<CircleCollider2D>();
            }
        }
    }
}
