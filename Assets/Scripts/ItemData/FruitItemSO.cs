using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Fru_", menuName = "WB/Fruit")]
public class FruitItemSO : ItemData
{
    public enum EFruitType
    {
        Savory,
        Pretty,
        Curse
    }

    public EFruitType FruitType;

    public Sprite GrowingSprite;
}
