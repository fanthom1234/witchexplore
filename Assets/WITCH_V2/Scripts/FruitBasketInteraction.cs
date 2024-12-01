using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBasketInteraction : AC.Interaction
{
    public FruitBasket FruitBasket;

    public override void Interact()
    {
        base.Interact();
        Debug.Log("FruitBasketInteraction");
       // FruitBasket.TryTurnOnAllHotspot();
    }
}
