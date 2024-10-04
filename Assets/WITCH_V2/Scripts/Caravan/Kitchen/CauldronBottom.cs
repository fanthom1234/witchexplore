using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronBottom : TriggerSensor
{
    private CakeIngredient _ing;
    private FruitCakeIngredient _ingf;

    protected override void OnSensorEnter(Collider2D other)
    {
        base.OnSensorEnter(other);
        if (other.gameObject.TryGetComponent<CakeIngredient>(out _ing))
        {
            EventBus.TriggerEvent(new IngredientToCauldronEvent(_ing.IngredientType));
        } 
        if (other.gameObject.TryGetComponent<FruitCakeIngredient>(out _ingf))
        {
            EventBus.TriggerEvent(new FruitToCauldronEvent(_ingf.FruitName));
        }
    }
}
