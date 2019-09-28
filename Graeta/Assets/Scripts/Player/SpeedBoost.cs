using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private bool isActive = false;

    private float speedBoostTimer = 0;
    public float speedBoostTimerLimit = 3;

    private bool onCooldown = false;

    private float cooldownTimer = 0;
    public float cooldownTimerLimit = 10;

    public Player.PlayerMovement playerMovement;

    void Update()
    {
        if (isActive)
        {
            speedBoostTimer += Time.deltaTime;
            if (speedBoostTimer > speedBoostTimerLimit)
            {
                speedBoostTimer = 0;
                isActive = false;
                playerMovement.resetSpeed();
            }
        }

        if (onCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > cooldownTimerLimit)
            {
                cooldownTimer = 0;
                onCooldown = false;
            }
        }
    }

    public void activate()
    {
        if (!onCooldown)
        {
            isActive = true;
            onCooldown = true;
            playerMovement.boostSpeed();
        }
    }

    public bool isOnCooldown()
    {
        return onCooldown;
    }

    public float remainingCooldown()
    {
        if (onCooldown)
        {
            return cooldownTimerLimit - cooldownTimer;
        }
        return 0;
    }
}
