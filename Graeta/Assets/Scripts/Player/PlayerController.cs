using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int currentHp;
    private int maxHp;

    public int maxHpIncrease;

    private bool isOnFire = false;
    private bool isPoisoned = false;
    private bool isBugged = false;

    private bool isInvisibleUpgradeAvailable = false;
    private bool isRegenerateUpgradeAvailable = false;

    private float fireTimer = 0;
    private float bugTimer = 0;

    public Player.PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Fire"))
        {

        } else if (collision.tag.Equals("Woodcutter"))
        {

        } else if (collision.tag.Equals("Bug"))
        {

        }
    }

    private void increaseMaxHp()
    {
        maxHp += maxHpIncrease;
    }

    private void changeHp(int value)
    {
        currentHp += value;
    }

    public bool isVisible()
    {
        // TODO: true == Tree nearby
        if(true && (!playerMovement.isMoving() || isInvisibleUpgradeAvailable)){
            return false;
        }
        return true;
    }
}
