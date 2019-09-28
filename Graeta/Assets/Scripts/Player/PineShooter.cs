using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineShooter : MonoBehaviour
{
    public GameObject aimDicator;
    public GameObject pine;
    public float shotDelay;
    public float lifeTime;
    public int speed;
    public bool canShoot;

    float timer;
    
    void Update()
    {
        if (!canShoot)
            return;

        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        if (!aimDicator.activeSelf && timer < 0)
        {
            aimDicator.SetActive(true);
        }

    }

    public void Shoot(Vector2 direction)
    {
        if (!canShoot || !aimDicator.activeSelf)
            return;

        if (aimDicator != null && direction != new Vector2(0, 0))
        {
            timer = shotDelay;
            direction = direction.normalized;
            GameObject shot = Instantiate(pine);
            shot.transform.position = aimDicator.transform.position;
            shot.transform.rotation = aimDicator.transform.rotation;
            shot.GetComponent<Rigidbody2D>().AddForce(direction * speed);
            shot.GetComponent<Rigidbody2D>().AddTorque(250);
            Destroy(shot, lifeTime);
            aimDicator.SetActive(false);
        }
    }

    public void TakeAim(Vector2 direction)
    {
        if (!canShoot)
        {
            return;
        }

        Vector3 targetVector = new Vector3(direction.x, direction.y);
        float angle = Vector3.Angle(Vector3.up, targetVector);
        if (targetVector.x < 0)
        {
            angle *= -1;
        }

        aimDicator.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
    }
}
