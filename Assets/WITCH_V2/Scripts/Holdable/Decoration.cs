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

    [ReadOnly]
    public bool IsFlip;

    private HoldableSpawningController _spawningController;

    protected override void Initialization()
    {
        base.Initialization();
        _spawningController = CoreGameManager.Instance.HoldableSpawningController;
    }

    public void SetDecorationSprite(Sprite sprite)
    {
        HoldableRenderer.sprite = sprite;
    }

    protected override void OnScrollWheel(int increment)
    {
        base.OnScrollWheel(increment);
        Rotate(increment);
    }

    public void Rotate(int increment)
    {
        transform.rotation *= Quaternion.AngleAxis(increment * RotatingAngle, Vector3.forward);
    }

    protected override void OnRightMouseDown()
    {
        base.OnRightMouseDown();
        _spawningController.ShiftLayer(this, -1);
    }

    public void Flip()
    {
        HoldableRenderer.flipX = !HoldableRenderer.flipX;
        IsFlip = HoldableRenderer.flipX;
    }

    public void ShiftLayer(int increment)
    {
        _spawningController.ShiftLayer(this, increment);
    }
}
