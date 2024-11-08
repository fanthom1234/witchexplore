using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewItem", menuName = "WB/Item")]
public class ItemData : ScriptableObject
{
    // public Decorator[] decs;

    public string DisplayName;
    [FormerlySerializedAs("sprite")]
    public Sprite Sprite;

    
}
