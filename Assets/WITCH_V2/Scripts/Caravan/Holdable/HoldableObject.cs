using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HoldableObject : CaravanObject
{
    public bool CanHoldByClicking;

    private bool _isHolding;

    protected override void OnMouseClick()
    {
        base.OnMouseClick();
        if (_isHolding == false && CanHoldByClicking)
        {
            DoHold();
        }
    }

    public void DoHold()
    {
        SpriteRenderer.enabled = true;
        _isHolding = true;
        EventBus.TriggerEvent(new ObjectHoldEvent(this));
        OnHolded();
    }

    protected virtual void OnHolded()
    {

    }

    public void DoRelease()
    {
        _isHolding = false;
        EventBus.TriggerEvent(new ObjectHoldEvent(null));
        OnReleased();
    }

    protected virtual void OnReleased()
    {
        Debug.Log("OnRelease");
    }

    protected void SetHoldingFalse() 
    {
        _isHolding = false;
    }
}
