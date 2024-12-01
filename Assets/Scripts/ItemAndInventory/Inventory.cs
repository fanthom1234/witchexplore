using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public struct InventoryFruitChanged
{

}

public class Inventory : Singleton<Inventory>
{
    [System.Serializable]
    public class InvenItem
    {
        public ItemData ItemData;
        public int Count = 1;
    }
    public List<Item> items;
    public List<BaseCakeSO> baseCakes;
    public List<InvenItem> FruitsInBasket;
    public List<DecorationItemSO> decorations;

    public void AddBaseCake(BaseCakeSO baseCake)
    {
        baseCakes.Add(baseCake);
    }

    public void AddFruit(FruitItemSO fruitItemSO)
    {
        foreach (InvenItem item in FruitsInBasket)
        {
            if (item.ItemData.DisplayName.Contains(fruitItemSO.DisplayName))
            {
                item.Count++;
                EventBus.TriggerEvent(new InventoryFruitChanged());
                return;
            }
        }
    }
}
