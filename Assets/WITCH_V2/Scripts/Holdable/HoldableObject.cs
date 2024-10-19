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

    private void Reset()
    {
        //Hotspot hotspot = null;
        //ActionEvent actionEvent = null;
        //if (TryGetComponent(out hotspot) && hotspot.useButtons.Count > 0 && hotspot.useButtons[0].interaction)
        //{
        //    foreach(AC.Action action in hotspot.useButtons[0].interaction.GetActions())
        //    {
        //        actionEvent = action as ActionEvent;
        //        if (actionEvent != null) 
        //        {
        //            return;
        //        }
        //    }
        //    hotspot.useButtons[0].interaction.actions[0] = new ActionEvent();
        //    actionEvent = hotspot.useButtons[0].interaction.actions[0] as ActionEvent;
        //    actionEvent.unityEvent = new UnityEngine.Events.UnityEvent();
        //}
    }

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
        Debug.Log("OnRelease");
        if (OnReleaseInteraction != null)
        {
            OnReleaseInteraction.Interact();
        }
    }

    protected void SetHoldingFalse() 
    {
        _isHolding = false;
    }
}
