using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBasket : CaravanObject, IEventSubcriber<InventoryFruitChanged>
{
    [SerializeField] FruitKitchenButtonPanel[] FruitKitchenButtonPanels;
    
    Inventory Inventory => Inventory.Instance;

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

    protected override void Initialization()
    {
        base.Initialization();
        EvaluateFruitInBasket();
    }

    private void EvaluateFruitInBasket()
    {
        for (int i = 0; i < FruitKitchenButtonPanels.Length; i++)
        {
            if (Inventory.FruitsInBasket[i].Count > 0)
            {
                FruitKitchenButtonPanels[i].gameObject.SetActive(true);
                FruitKitchenButtonPanels[i].SetFruit(Inventory.FruitsInBasket[i]);
            }
            else
            {
                FruitKitchenButtonPanels[i].gameObject.SetActive(false);
            }
        }
    }

    //public void TryTurnOnAllHotspot()
    //{
    //    foreach (FruitCakeIngredient fruit in HoldableFruits)
    //    {
    //        fruit.TryTurnOnHotspot();
    //    }
    //}

    public void OnEventBusTrigger(InventoryFruitChanged eventType)
    {
        EvaluateFruitInBasket();
    }
}
