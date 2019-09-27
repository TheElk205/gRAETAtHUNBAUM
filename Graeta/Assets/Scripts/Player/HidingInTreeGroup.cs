using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingInTreeGroup : MonoBehaviour
{
    public bool isPlayerInTreeGroup;

    void Start()
    {
        isPlayerInTreeGroup = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("TreeGroup"))
        {
            this.isPlayerInTreeGroup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("TreeGroup"))
        {
            this.isPlayerInTreeGroup = false;
        }
    }
}
