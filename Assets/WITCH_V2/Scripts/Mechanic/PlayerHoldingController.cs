using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObjectHoldEvent
{
    public HoldableObject Holdable;
    public float Cooldown;

    public ObjectHoldEvent(HoldableObject holdable)
    {
        Holdable = holdable;
        Cooldown = 1;
    }
}

public class PlayerHoldingController : CaravanObject, IEventSubcriber<ObjectHoldEvent>
{
    [SerializeField] float FollowSpeed;

    private HoldableObject _currHolding;
    private Vector3 _mousePosition;

    public bool OneFramePassed;

    public bool CurrentHolding { get; internal set; }

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<ObjectHoldEvent>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<ObjectHoldEvent>(this);
    }

    public void SetHolding(HoldableObject holdable)
    {
        _currHolding = holdable;
    }

    public void OnEventBusTrigger(ObjectHoldEvent eventType)
    {
        SetHolding(eventType.Holdable);
        OneFramePassed = false;
    }

    protected override void OnFrameStart()
    {
        base.OnFrameStart();
        if (_currHolding == null)
        {
            return;
        }
        if (OneFramePassed == false)
        {
            OneFramePassed = true;
            return;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // scroll forward
        {
            _currHolding.DoScrollWheel(1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < -0f) // scroll backwards
        {
            _currHolding.DoScrollWheel(-1);
        }
        if (Input.GetMouseButtonDown(1)) // right mouse down
        {
            _currHolding.DoRightMouseDown();
        }
        if (Input.GetMouseButtonDown(0))
        {
            _currHolding.DoRelease();
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (_currHolding == null)
        {
            return;
        }

        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

        // Ensure only x and y are used from _mousePosition
        Vector2 targetPosition = new Vector2(_mousePosition.x, _mousePosition.y);

        // Smoothly move the object to the mouse position using Lerp
        _currHolding.transform.position = Vector2.Lerp(_currHolding.transform.position, targetPosition, FollowSpeed);
    }

    public void TryDestroyHolding()
    {
        if (_currHolding != null)
        {
            Destroy(_currHolding.gameObject);
            _currHolding = null;
        }
    }
}
