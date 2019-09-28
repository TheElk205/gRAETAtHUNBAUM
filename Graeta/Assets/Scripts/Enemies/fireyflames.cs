using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireyflames : MonoBehaviour
{
    public GameObject waypoint;
    public bool cycle;
    public Vector2[] waypoints;
    public float speed;
    public float spawnDelay = 0.2f;
    public float decayTime;
    public Rigidbody2D rb;
    public GameObject flameling;
    int counter;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        Vector2[] temp = waypoints;
        if (!cycle)
        {
            waypoints = new Vector2[temp.Length * 2];

            Vector2 startvector = transform.position;

            waypoints[0] = startvector;

            for (int i = 0; i < temp.Length; i++)
            {
                waypoints[i + 1] = temp[i] + startvector;
            }

            for (int i = 1; i < temp.Length; i++)
            {
                waypoints[temp.Length + i] = temp[temp.Length - i - 1];
            }
        }
        else
        {
            waypoints = new Vector2[temp.Length +1];

            waypoints[0] = new Vector2(0, 0);

            for (int i = 0; i < temp.Length; i++)
            {
                waypoints[i + 1] = temp[i];
            }
        }

        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypoint();
        SpawnFlame();
    }

    void MoveToWaypoint()
    {
        Vector2 direction = new Vector2(waypoints[counter].x - transform.position.x, waypoints[counter].y - transform.position.y).normalized;

        rb.AddForce(direction * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == waypoint)
        {
            counter++;
            counter %= waypoints.Length;
            waypoint.transform.position = waypoints[counter];
        }
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
