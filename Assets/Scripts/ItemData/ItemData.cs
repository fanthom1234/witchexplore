using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class ItemData : ScriptableObject
{
    public string DisplayName;
    [FormerlySerializedAs("sprite")]
    public Sprite Sprite;
}
