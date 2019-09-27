using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bug : MonoBehaviour
{
    public float speed = 10;
    public float chaseMod = 1;
    public int dmg = 1;
    public bool debug = false;
    public float timeReset = 2;
    public Rigidbody2D rb;
    public CircleCollider2D home;
    public CircleCollider2D range;
    Vector2 target;
    GameObject player;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
            debug = true;
        #endif

        timer = timeReset;
    }

    // Update is called once per frame
    void Update()
    {
        //Wenn Spieler sichtbar und in Reichweite bewege richtung spieler
        if ((player != null))
        {
            target = player.transform.position;
        }
        else
        {
            //Sonst bestimme eine Punkt zu den sich bewegt werden soll
            //bestimme punkt nur alle X Sekunden
            if (timer >= timeReset)
            {
                target = (Random.insideUnitCircle * home.radius);
                target.x += home.transform.position.x;
                target.y += home.transform.position.y;
                timer = 0;
            }
            else
                timer += Time.deltaTime;
        }

        //Bewege Bug auf ein Ziel zu
        MoveToTarget(target);
    }

    void MoveToTarget(Vector2 target)
    {
        Vector2 direction;

        if (player == null)
            direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        else
        {
            direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y) * chaseMod;
        }

        //Wenn in debuging mode zeichne Bewegungsrichtung ein
        if (debug)
        {
            ShowDirection(direction);
        }

        //Bewege Bug zum Ziel
        rb.AddForce(direction * (speed * Time.deltaTime));
    }

    void ShowDirection(Vector2 direction)
    {
        Debug.DrawRay(transform.position, direction, Color.red);
    }

    //Spieler betritt reichweite des Bugs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
            player = collision.gameObject;
    }

    //Spieler verlässt reichweite des Bugs
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            player = null;
            timer = timeReset;
        }
    }
}
