using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public List<Item> items;
    public List<BaseCakeSO> baseCakes;
    public List<FruitItemSO> FruitsInBasket;
    public List<DecorationItemSO> decorations;

    public void AddBaseCake(BaseCakeSO baseCake)
    {
        baseCakes.Add(baseCake);
    }
}
