using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode fireKey;
    public KeyCode speedBoostKey;
    public float wiggleLimit;
    float wiggleTimer;
    Vector2 lastInput = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 MoveDirection()
    {
        Vector2 direction;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        return direction;
    }

    public bool StandsStill()
    {
        if (MoveDirection().Equals(new Vector2(0, 0)))
        {
            return true;
        }

        return false;
    }

    public bool Shoot()
    {
        if (Input.GetKeyUp(fireKey)|| Input.GetAxis("Right Trigger") > 0)
        {
            return true;
        }

        return false;
    }

    public Vector2 ShotDirection()
    {
        Vector2 direction = new Vector2(0, 0);

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction.x -= transform.position.x;
        direction.y -= transform.position.y;
        
        if (Input.GetAxis("Aim X") > 0.03 || Input.GetAxis("Aim X") < -0.03 || Input.GetAxis("Aim Y") > 0.03 || Input.GetAxis("Aim Y") < -0.03)
        {
            direction.x = Input.GetAxis("Aim X");
            direction.y = Input.GetAxis("Aim Y");
        }

        return direction;
    }

    public int Wiggels()
    {
        Vector2 thisInput = MoveDirection();

        if (lastInput == new Vector2(0, 0) && thisInput != new Vector2(0, 0))
        {
            lastInput = thisInput;
            wiggleTimer = 0;
            return 1;
        }

        if (wiggleTimer > wiggleLimit)
        {
            lastInput = new Vector2(0, 0);
            return -1;
        }

        if (InputIsDiferent(lastInput, thisInput) && thisInput != new Vector2(0, 0))
        {
            lastInput = thisInput;
            wiggleTimer = 0;
            return 1;
        }

        wiggleTimer += Time.deltaTime;
        return 0;
    }

    bool InputIsDiferent(Vector2 in1, Vector2 in2)
    {
        if (in1.x > 0 && in2.x <= 0)
            return true;

        if (in1.x < 0 && in2.x >= 0)
            return true;

        if (in1.x == 0 && in2.x != 0)
            return true;

        if (in1.y == 0 && in2.y != 0)
            return true;

        if (in1.y > 0 && in2.y <= 0)
            return true;

        if (in1.y < 0 && in2.y >= 0)
            return true;

        return false;
    }

    public bool speedBoostButtonPressed()
    {
        if (Input.GetKeyDown(speedBoostKey))
        {
            return true;
        }
        if (Input.GetAxis("SpeedBoostAxis") < 0){
            return true;
        }
        return false;
    }
}
