using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class flamelings : MonoBehaviour
{
    float decay = 1;

    // Start is called before the first frame update
    void Start()
    {
        decay = transform.parent.GetComponentInChildren<Fireyflames>().decayTime;  
    }

    // Update is called once per frame
    void Update()
    {
        decay -= Time.deltaTime;

        if (decay < 0)
        {
            Destroy(gameObject);
        }
    }
}
