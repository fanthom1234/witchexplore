using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public List<Item> items;
    public List<FruitItemSO> FruitsInBasket;
}
