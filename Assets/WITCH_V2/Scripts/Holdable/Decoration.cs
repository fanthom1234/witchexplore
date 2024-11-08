using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public struct DecorationClickEvent
{
    public Decoration Decoration;

    public DecorationClickEvent(Decoration decoration)
    {
        Decoration = decoration;
    }
}

public class Decoration : HoldableObject
{
    public float RotatingAngle = 45;

    [Header("Reference")]
    public Hotspot Hotspot;
    public BoxCollider2D BoxCollider2D;

    [ReadOnly]
    public bool IsFlip;

    private HoldableSpawningController _spawningController;

    protected override void Initialization()
    {
        base.Initialization();
        _spawningController = CoreGameManager.Instance.HoldableSpawningController;
    }

    protected override void OnHolded()
    {
        base.OnHolded();
        Hotspot.enabled = false;
    }

    protected override void OnAfterReleaseCooldown()
    {
        base.OnAfterReleaseCooldown();
        Hotspot.enabled = true;
    }

    public void SetDecorationSprite(Sprite sprite)
    {
        HoldableRenderer.sprite = sprite;
    }

    public void SetDecorationInteracableSize(Vector2 size)
    {
        BoxCollider2D.size = size;
    }

    protected override void OnScrollWheel(int increment)
    {
        base.OnScrollWheel(increment);
        Rotate(increment);
    }

    public void Rotate(int increment)
    {
        HoldableRenderer.transform.rotation *= Quaternion.AngleAxis(increment * RotatingAngle, Vector3.forward);
    }

    protected override void OnRightMouseDown()
    {
        base.OnRightMouseDown();
        _spawningController.ShiftLayer(this, -1);
    }

    public void Flip()
    {
        IsFlip = !IsFlip;
        HoldableRenderer.transform.rotation = Quaternion.Euler(0, IsFlip ? 180 : 0, HoldableRenderer.transform.localEulerAngles.z);
    }

    public void ShiftLayer(int increment)
    {
        _spawningController.ShiftLayer(this, increment);
    }

    public void Duplicate()
    {
        _spawningController.SpawnHoldable(this as HoldableObject, ReleaseArea, HoldableRenderer.sortingLayerName);
    }

    public void Delete()
    {
        _spawningController.Delete(this as HoldableObject, HoldableRenderer.sortingOrder, HoldableRenderer.sortingLayerName);
    }
}
