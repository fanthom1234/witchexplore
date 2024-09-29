using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "WB/Item")]
public class ItemData : ScriptableObject
{
    // public Decorator[] decs;

    public String itemName;
    public Sprite sprite;
}
