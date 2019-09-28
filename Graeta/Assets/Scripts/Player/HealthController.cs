using System.Collections;
using System.Collections.Generic;
using SceneTransition;
using UnityEngine;

namespace Player
{
    public class HealthController : MonoBehaviour
    {
        public float regenIntervalInSeconds;
        public int startHealth;
        public MaxHealth startMaxHealth;

        public GameObject health0;
        public GameObject health1;
        public GameObject health2;
        public GameObject health3;
        public GameObject health4;
        public GameObject health5;
        public GameObject health6;

        public Sprite fullSprite;
        public Sprite halveSprite;
        public Sprite emptySprite;

        public enum MaxHealth
        {
            SIX_HEALTH = 6,
            TEN_HEALTH = 10,
            FOURTEEN_HEALTH = 14
        }

        private List<GameObject> healthObjects = new List<GameObject>();
        private int listOffset = 0;

        private int maxHealth = 0;
        private float timer = 0.0f;
        private int health = 0;
        private bool isRegenEnabled = false;


        private void Start()
        {
            this.fillHealthObjects();

            this.setMaxHealth(this.startMaxHealth);
            this.setHealth(this.startHealth);
        }

        public void setMaxHealth(MaxHealth maxHealth)
        {
            if (this.maxHealth == (int) maxHealth)
            {
                return;
            }

            this.maxHealth = (int) maxHealth;

            setAllActive();
            listOffset = 0;

            if (this.maxHealth <= 10)
            {
                health0.SetActive(false);
                health6.SetActive(false);
                listOffset = 1;
            }

            if (this.maxHealth <= 6)
            {
                health1.SetActive(false);
                health5.SetActive(false);
                listOffset = 2;
            }

            this.setHealth(this.health);
        }

        public MaxHealth getMaxHealth()
        {
            return (MaxHealth) this.maxHealth;
        }

        public int getHealth()
        {
            return this.health;
        }

        public void setHealth(int health)
        {
            if (health > this.maxHealth)
            {
                return;
            }

            this.health = health;

            this.setAllHealthToEmptySprite();
            for (int i = 1; i <= health; i++)
            {
                int index = this.listOffset + ((i - 1) / 2);
                int fullOrHalve = i % 2;

                if (fullOrHalve == 0)
                {
                    healthObjects[index].GetComponent<SpriteRenderer>().sprite = fullSprite;
                }
                else
                {
                    healthObjects[index].GetComponent<SpriteRenderer>().sprite = halveSprite;
                }
            }

            if (health <= 0)
            {
                SceneManager.sceneManager.Restart();
            }
        }

        public void setEnableRegen(bool isEnabled)
        {
            this.isRegenEnabled = isEnabled;
        }

        public bool getIsRegenEnabled()
        {
            return this.isRegenEnabled;
        }

        private void fillHealthObjects()
        {
            healthObjects.Add(health6);
            healthObjects.Add(health5);
            healthObjects.Add(health4);
            healthObjects.Add(health3);
            healthObjects.Add(health2);
            healthObjects.Add(health1);
            healthObjects.Add(health0);
        }

        private void setAllActive()
        {
            foreach (GameObject health in healthObjects)
            {
                health.SetActive(true);
            }
        }

        private void setAllHealthToEmptySprite()
        {
            foreach (GameObject health in healthObjects)
            {
                health.GetComponent<SpriteRenderer>().sprite = emptySprite;
            }
        }

        void Update()
        {
            if (this.isRegenEnabled)
            {
                this.handleRegenTimer();
            }

        }

        private void handleRegenTimer()
        {
 
            timer += Time.deltaTime;

            if (timer > regenIntervalInSeconds)
            {
                timer = timer - regenIntervalInSeconds;
                setHealth(health + 1);
            }
        }

    }

}