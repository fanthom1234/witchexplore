using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory playerInventory;
    public bool isPlayerInventory;
    public List<Item> items;

    private void Awake() {
        if (isPlayerInventory)
            playerInventory = this;
    }

}
