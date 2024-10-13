using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBasket : MonoBehaviour
{
    [SerializeField] FruitCakeIngredient[] HoldableFruits;
    [SerializeField] Inventory Inventory => Inventory.Instance;

    private void Start()
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
}
