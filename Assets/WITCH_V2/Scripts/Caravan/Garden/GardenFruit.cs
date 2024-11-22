using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public struct GardenFruitPickedUp
{
    public Marker Marker;

    public GardenFruitPickedUp(Marker marker)
    {
        Marker = marker;
    }
}

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
    private Marker _currPosMarker;

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
            Renderer.color = Color.white;
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
        EventBus.TriggerEvent(new GardenFruitPickedUp(_currPosMarker));
    }

    public void SetPositionMarker(Marker m)
    {
        transform.position = m.Position;
        _currPosMarker = m;
    }
}
