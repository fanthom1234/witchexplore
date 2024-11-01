using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldableObject : CaravanObject
{
    [Header("Object Reference")]
    [SerializeField] protected SpriteRenderer HoldableRenderer;

    [Header("Optional Reference")]
    [SerializeField] protected ReleaseHoldableBound ReleaseArea;
    [SerializeField] Interaction OnReleaseInteraction;

    private bool _isHolding;

    public bool _isOnOneFrameCooldown { get; private set; }

    protected override void Initialization()
    {
        base.Initialization();
        if (HoldableRenderer == null)
        {
            HoldableRenderer = SpriteRenderer;
        }
    }

    public void DoHold()
    {
        if (_isOnOneFrameCooldown)
        {
            return;
        }
        if (HoldableRenderer)
        {
            HoldableRenderer.enabled = true;
        }
        _isHolding = true;
        EventBus.TriggerEvent(new ObjectHoldEvent(this));
        OnHolded();
    }

    protected virtual void OnHolded()
    {

    }

    public void DoRelease()
    {
        if (ReleaseArea == null)
        {
            _isHolding = false;
            EventBus.TriggerEvent(new ObjectHoldEvent(null));
            OnReleased();
        }
        else if (Vector2.Distance(transform.position, ReleaseArea.transform.position) < ReleaseArea.Radius)
        {
            _isHolding = false;
            EventBus.TriggerEvent(new ObjectHoldEvent(null));
            OnReleased();
        }
    }

    protected virtual void OnReleased()
    {
        StartCoroutine(CooldownRoutine());
        if (OnReleaseInteraction != null)
        {
            OnReleaseInteraction.Interact();
        }
    }

    IEnumerator CooldownRoutine()
    {
        _isOnOneFrameCooldown = true;
        yield return new WaitForEndOfFrame();
        _isOnOneFrameCooldown = false;
    }

    protected void SetHoldingFalse() 
    {
        _isHolding = false;
    }

    public void DoScrollWheel(int increment)
    {
        OnScrollWheel(increment);
    }

    public void DoRightMouseDown()
    {
        OnRightMouseDown();
    }

    protected virtual void OnScrollWheel(int increment)
    {

    }

    protected virtual void OnRightMouseDown()
    {

    }

    public void SetDecorationReleaseBound(ReleaseHoldableBound releaseBound)
    {
        ReleaseArea = releaseBound;
    }
}
