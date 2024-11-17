using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBasket : CaravanObject, IEventSubcriber<InventoryFruitChanged>
{
    [SerializeField] FruitCakeIngredient[] HoldableFruits;
    [SerializeField] Inventory Inventory => Inventory.Instance;

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<InventoryFruitChanged>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<InventoryFruitChanged>(this);
    }

    private void Start()
    {
        EvaluateFruitInBasket();
    }

    private void EvaluateFruitInBasket()
    {
        for (int i = 0; i < HoldableFruits.Length; i++)
        {
            FruitCakeIngredient fruitObject = HoldableFruits[i];
            if (i >= Inventory.FruitsInBasket.Count)
            {
                fruitObject.SetFruitData(null);
            }
            else
            {
                fruitObject.SetFruitData(Inventory.FruitsInBasket[i]);
            }
        }
    }

    public void TryTurnOnAllHotspot()
    {
        foreach (FruitCakeIngredient fruit in HoldableFruits)
        {
            fruit.TryTurnOnHotspot();
        }
    }

    public void OnEventBusTrigger(InventoryFruitChanged eventType)
    {
        EvaluateFruitInBasket();
    }
}
