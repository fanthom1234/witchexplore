using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : HoldableObject
{
    public void SetDecorationSprite(Sprite sprite)
    {
        HoldableRenderer.sprite = sprite;
    }
}
