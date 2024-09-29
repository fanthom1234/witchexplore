using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform content;
    public ItemUI itemUIPrefab;

    private void Start() {
        Display(inventory);
    }

    public virtual void Display(Inventory inventory)
    {
        this.inventory = inventory;
        Refresh();
    }

    public virtual void Refresh()
    {
        foreach (Item item in inventory.items)
        {
            ItemUI itemUI = ItemUI.Instantiate(itemUIPrefab, content);
            itemUI.Display(item);
        }
    }
}
