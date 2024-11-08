using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Decor_", menuName = "WB/Decoration")]
public class DecorationItemSO : ItemData
{
    public enum EDecorationType
    {
        Savory,
        Pretty,
        Curse
    }

    public EDecorationType Type;
    public float Size = 1.5f;

    [Flags]
    public enum ETag
    {
        None,
        Pizza,
        Mushroom,
        Vegetable,
        Fruit,

        
    }

    public ETag Tags;
}
