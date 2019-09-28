using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Collectibles;

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
    public PineShooter shooter;
    public Player.HealthController healthController;
    public SpeedBoost speedBoost;

    public GameObject burning, bugged;
    int wiggeldCount = 10;
    public int wiggeldOf = 10;
    private CollectiblesManager collectiblesManager;

    public static PlayerController instance { private set; get; }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
            
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<PineShooter>(); 
        collectiblesManager = FindObjectOfType<CollectiblesManager>();
        inputManager = GetComponent<InputManager>();
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

        if (isBugged)
        {
            if (inputManager.Wiggels() == 1)
            {
                wiggeldCount += 1;
            }
            else if (inputManager.Wiggels() == -1)
            {
                wiggeldCount = 0;
            }

            if (wiggeldCount >= wiggeldOf)
            {
                wiggeldCount = 0;
                isBugged = false;
                bugged.SetActive(false);
            }
        }
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Fire"))
        {
            isOnFire = true;
			FindObjectOfType<AudioManager>().Play("FireHit");
        }
        else if (collision.tag.Equals("Bug"))
        {
            isBugged = true;
            bugged.SetActive(true);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Water"))
        {
            isOnFire = false;
            burning.SetActive(false);
        }
        else if (collision.gameObject.tag.Equals("ShopItem"))
        {
            var shopItem = collision.gameObject.GetComponent<ShopItem>();
            Debug.Log("ShopItem: " + shopItem.type.ToString());
            Debug.Log("ShopItem price: " + shopItem.price.ToString());
            Debug.Log("Resources: " + this.collectiblesManager.collectedResources.ToString());

            if (this.collectiblesManager.collectedResources < shopItem.price)
            {
                Debug.Log("Lacking resources!");
                return;
            }

            switch (shopItem.type)
            {
                case ShopItem.ItemType.SPEED:
                    this.isSpeedBoostUpgradeAvailable = true;
                    break;
                case ShopItem.ItemType.SHOOT:
                    this.shooter.canShoot = true;
                    break;
                case ShopItem.ItemType.HEALTH:
                    var maxHealth = this.healthController.getMaxHealth();
                    if (maxHealth == Player.HealthController.MaxHealth.SIX_HEALTH)
                    {
                        this.healthController.setMaxHealth(Player.HealthController.MaxHealth.TEN_HEALTH);
                    }
                    else if (maxHealth == Player.HealthController.MaxHealth.TEN_HEALTH)
                    {
                        this.healthController.setMaxHealth(Player.HealthController.MaxHealth.FOURTEEN_HEALTH);
                    }
                    else if (maxHealth == Player.HealthController.MaxHealth.FOURTEEN_HEALTH)
                    {
                        Debug.Log("Already max health!");
                        return;
                    }
                    break;
                case ShopItem.ItemType.STEALTH:
                    this.isInvisibleUpgradeAvailable = true;
                    break;
            }

            this.collectiblesManager.collectedResources -= shopItem.price;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Woodcutter"))
        {
            changeHp(-collision.gameObject.GetComponent<woodcutter>().dmg);
            FindObjectOfType<AudioManager>().Play("LumberjackHit");
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
