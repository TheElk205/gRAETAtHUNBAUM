using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineCone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player"))
            GetComponent<CapsuleCollider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<AudioManager>().Play("PineconeHit");
    }
}
