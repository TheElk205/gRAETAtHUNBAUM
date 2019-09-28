using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public enum ItemType
    {
        SPEED,
        SHOOT,
        HEALTH,
        STEALTH
    }

    public ItemType type = ItemType.SPEED;
    public int price = 1;
}
