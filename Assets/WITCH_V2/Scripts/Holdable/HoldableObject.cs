using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldableObject : CaravanObject
{
    [Header("Object Reference")]
    public SpriteRenderer HoldableRenderer;

    [Header("Optional Reference")]
    [SerializeField] protected ReleaseHoldableBound ReleaseArea;
    [SerializeField] Interaction OnReleaseInteraction;

    private bool _isHolding;
    private Vector3 _layeredPos;

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
        else if (ReleaseArea.CheckIsObjectOnBound(transform.position))
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
        OnAfterReleaseCooldown();
    }

    protected virtual void OnAfterReleaseCooldown()
    {

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

    public void SetSortingLayer(string sortingLayerName, int sortingOrder)
    {
        HoldableRenderer.sortingLayerName = sortingLayerName;
        HoldableRenderer.sortingOrder = sortingOrder;
        _layeredPos = HoldableRenderer.transform.position;
        _layeredPos.z = sortingOrder;
        HoldableRenderer.transform.position = _layeredPos;
    }
}
