using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : HoldableObject
{
    public float RotatingAngle = 45;

    public void SetDecorationSprite(Sprite sprite)
    {
        HoldableRenderer.sprite = sprite;
    }

    protected override void OnScrollWheel(int increment)
    {
        base.OnScrollWheel(increment);
        transform.rotation *= Quaternion.AngleAxis(increment * RotatingAngle, Vector3.forward);
    }
}
