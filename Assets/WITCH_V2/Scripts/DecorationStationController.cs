using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorationStationController : CaravanObject, IEventSubcriber<RoomEnteredEvent>
{
    public enum EShelfType
    {
        BaseCake,
        Pretty,
        Cursed, 
        Disgusting, 
        Savory,
        Favourite
    }

    public EShelfType CurrentShelf;

    [Header("Asset Reference")]
    public HoldableObject BaseCakePrefab;
    [Header("Sceen Object Reference")]
    public GridLayoutGroup GridLayoutGroup;


    private Inventory _inventory;
    private List<Image> _items;

    protected override void Initialization()
    {
        base.Initialization();
        _inventory = Inventory.Instance;
        _items = new List<Image>();
        foreach (Image image in GridLayoutGroup.GetComponentsInChildren<Image>())
        {
            if (image != GridLayoutGroup)
            {
                _items.Add(image);
            }
        }
    }

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe(this);
        //UpdateFromInventory();
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe(this);
    }

    public void OnEventBusTrigger(RoomEnteredEvent eventType)
    {
        if (eventType.ToRoom == CaravanRoom.ERoom.Decoration)
        {
            Debug.Log("Callpop");
            PopulateInventory();
        }
    }

    //private void UpdateFromInventory()
    //{
    //    for (int i = 0; i < Markers.Length; i++) 
    //    {
    //        if (Markers[i] != null && i < _inventory.baseCakes.Count)
    //        {
    //            HoldableObject holdableObject = Instantiate(BaseCakePrefab, Markers[i].transform.position, Quaternion.identity);
    //            SpriteRenderer spriteRenderer;
    //            if (holdableObject.TryGetComponent<SpriteRenderer>(out spriteRenderer))
    //            {
    //                spriteRenderer.sprite = _inventory.baseCakes[i].CakeSprite;
    //            }
    //        }
    //    }
    //}

    private void PopulateInventory()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            Image ite = _items[i];
            Debug.Log("Compare " + i + " >= " + _inventory.baseCakes.Count);
            if (i >= _inventory.baseCakes.Count)
            {
                ite.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("set picture" + ite.name);
                ite.gameObject.SetActive(true);
                ite.sprite = _inventory.baseCakes[i].CakeSprite;
            }
        }
        
    }
}
