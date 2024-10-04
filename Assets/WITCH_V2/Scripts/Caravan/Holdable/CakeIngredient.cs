using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeIngredient : HoldableObject
{
    public Ingredient.EType IngredientType;

    private Rigidbody2D rigidbody2d;

    protected override void Initialization()
    {
        base.Initialization();
        if (TryGetComponent(out rigidbody2d))
        {
            rigidbody2d.gravityScale = 0;
        }
    }

    protected override void OnReleased()
    {
        base.OnReleased();
        rigidbody2d.velocity = Vector3.zero;
        rigidbody2d.gravityScale = 1;
    }
}
