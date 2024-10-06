using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkCakeIngredient : HoldableObject
{
    private Vector3 _initialPos;
    private Quaternion _initialRot;

    protected override void Initialization()
    {
        base.Initialization();
        _initialPos = transform.position;
        _initialRot = transform.rotation;
    }
    protected override void OnReleased()
    {
        base.OnReleased();
        EventBus.TriggerEvent(new IngredientToCauldronEvent(Ingredient.EType.Milk));
    }

    public void ResetPosition()
    {
        transform.position = _initialPos;
        transform.rotation = _initialRot;
    }
}
