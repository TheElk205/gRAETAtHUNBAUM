using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isOnFire = false;
    // private bool isPoisoned = false;
    private bool isBugged = false;

    public bool isInvisibleUpgradeAvailable = false;
    public bool isRegenerateUpgradeAvailable = false;
    public bool isSpeedBoostUpgradeAvailable = false;

    private float fireTimer = 0;
    private float bugTimer = 0;
    private float poisonTimer = 0;
    public float fireTimerLimit;
    public float bugTimerLimit;
    public float poisonTimerLimit;

    public Player.PlayerMovement playerMovement;
    public HidingInTreeGroup hidingInTreeGroup;
    public InputManager inputManager;
    PineShooter shooter;
    public Player.HealthController healthController;
    public SpeedBoost speedBoost;

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<PineShooter>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        if (isOnFire)
        {
            fireTimer += deltaTime;
            if (fireTimer > fireTimerLimit)
            {
                changeHp(-1);
                fireTimer = 0;
            }
        }
        if (isBugged)
        {
            bugTimer += deltaTime;
            if (bugTimer > bugTimerLimit)
            {
                changeHp(-1);
                bugTimer = 0;
            }
        }

        if (shooter != null)
        {
            Vector2 direction = inputManager.ShotDirection();
            shooter.TakeAim(direction);

            if (inputManager.Shoot())
            {
                shooter.Shoot(direction);
            }
        }

        if(isRegenerateUpgradeAvailable && !healthController.getIsRegenEnabled())
        {
            healthController.setEnableRegen(true);
        }
        if(isSpeedBoostUpgradeAvailable && inputManager.speedBoostButtonPressed())
        {
            speedBoost.activate();
        }
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Fire"))
        {
            isOnFire = true;
        } else if (collision.tag.Equals("Bug"))
        {
            isBugged = true;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Woodcutter"))
        {
            changeHp(-collision.gameObject.GetComponent<woodcutter>().dmg);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // TODO: Does not continously update or only when player is moving inside poison?
        if (other.gameObject.tag.Equals("Poison"))
        {
            poisonTimer += Time.deltaTime;
            if(poisonTimer > poisonTimerLimit)
            {
                changeHp(-1);
                poisonTimer = 0;
            }
        }
    }

    private void changeHp(int value)
    {
        int currentHealth = healthController.getHealth();
        healthController.setHealth(currentHealth + value);
    }

    public bool isVisible()
    {
        if(hidingInTreeGroup.isPlayerInTreeGroup && (!playerMovement.isMoving() || isInvisibleUpgradeAvailable))
        {
            return false;
        }
        return true;
    }

    /*private void resolveTimer(float timer, float timerLimit, int dmg)
    {
        Debug.Log("Timer = " + timer);
        Debug.Log("DeltaTime " + Time.deltaTime);
        timer += Time.deltaTime;
        if (timer > timerLimit)
        {
            changeHp(-dmg);
            timer = 0;
        }
    }*/
}
