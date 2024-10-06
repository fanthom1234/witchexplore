using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCakeIngredient : HoldableObject, IEventSubcriber<FruitToCauldronEvent>
{
    public string FruitName;

    private Rigidbody2D rigidbody2d;
    private Hotspot _hotspot;
    private Vector3 _startPos;

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<FruitToCauldronEvent>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<FruitToCauldronEvent>(this);
    }

    public void OnEventBusTrigger(FruitToCauldronEvent eventType)
    {
        if (eventType.FruitCakeIngredient.IsEqual(this))
        {
            Invoke("Hide", .1f);
        }
    }

    private bool IsEqual(FruitCakeIngredient fruitCakeIngredient)
    {
        return this.GetInstanceID() == fruitCakeIngredient.GetInstanceID(); ;
    }

    protected override void Initialization()
    {
        base.Initialization();
        TryGetComponent(out rigidbody2d);
        TryGetComponent(out _hotspot);
        _startPos = transform.position;
        Hide();
    }

    private void Hide()
    {
        rigidbody2d.gravityScale = 0;
        rigidbody2d.velocity = Vector2.zero;
        SpriteRenderer.enabled = false;
        transform.position = _startPos;
        _hotspot.TurnOff();
    }

    protected override void OnReleased()
    {
        base.OnReleased();
        rigidbody2d.velocity = Vector3.zero;
        rigidbody2d.gravityScale = 1;
        StartCoroutine(DisableHotspotRoutine());
    }

    IEnumerator DisableHotspotRoutine()
    {
        _hotspot.TurnOff();
        yield return new WaitForEndOfFrame();
        _hotspot.TurnOn();
    }
}
