using System;
using Unity.Collections;
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

    public enum ETag
    {
        None,
        Candy,
        Mushroom,Flowers,Creatures,BloodBones,Plant,Egg,Meat,Seafood
    }

    public ETag[] Tags;
}
