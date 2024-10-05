using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class CakeIngredient : HoldableObject, IEventSubcriber<IngredientToCauldronEvent>
{
    public Ingredient.EType IngredientType;

    private Rigidbody2D rigidbody2d;
    private Vector3 _startPos;
    private Vector3 _hidingPos;

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<IngredientToCauldronEvent>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<IngredientToCauldronEvent>(this);
    }

    public void OnEventBusTrigger(IngredientToCauldronEvent eventType)
    {
        Invoke("Hide", .1f);
    }

    protected override void Initialization()
    {
        base.Initialization();
        TryGetComponent(out rigidbody2d);
        _startPos = transform.position;
        _hidingPos = transform.position + (Vector3.down * 10);
        Hide();
    }

    private void Hide()
    {
        rigidbody2d.gravityScale = 0;
        rigidbody2d.velocity = Vector2.zero;
        SpriteRenderer.enabled = false;
        transform.position = _hidingPos;
    }

    protected override void OnHolded()
    {
        base.OnHolded();
        transform.position = _startPos;
    }

    protected override void OnReleased()
    {
        base.OnReleased();
        rigidbody2d.velocity = Vector3.zero;
        rigidbody2d.gravityScale = 1;
    }
}
