using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;



public class GardenFruit : CaravanObject
{
    public FruitItemSO FruitItemSO;
    public EStage CurrentStage;

    public enum EStage
    {
        None,
        Growing,
        FullyGrown
    }

    [Header("Object Reference")]
    public SpriteRenderer Renderer;
    public Hotspot Hotspot;

    private Inventory _inventory;

    protected override void Initialization()
    {
        base.Initialization();
        _inventory = Inventory.Instance;
    }

    public void SetFruit(FruitItemSO fruitItemSO)
    {
        FruitItemSO = fruitItemSO;
        SetStage(EStage.FullyGrown);
        Invoke("ChangeHotSpotName", 0.001f);
    }

    private void ChangeHotSpotName()
    {
        Hotspot.SetName(FruitItemSO.DisplayName, Hotspot.displayLineID);
    }

    private void EvaluateStage()
    {
        if (CurrentStage == EStage.FullyGrown)
        {
            Renderer.sprite = FruitItemSO.Sprite;
            Hotspot.enabled = true;
        }
        else if (CurrentStage == EStage.Growing)
        {
            Hotspot.enabled = false;
            Renderer.sprite = FruitItemSO.GrowingSprite;
        }
    }

    public void SetStage(EStage stage)
    {
        CurrentStage = stage;
        EvaluateStage();
    }

    public void DoPickUp()
    {
        _inventory.AddFruit(FruitItemSO);
    }
}
