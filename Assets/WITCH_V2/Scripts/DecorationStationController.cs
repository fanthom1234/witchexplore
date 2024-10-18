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
    private List<DecorationButtonPanel> _decPanels;

    protected override void Initialization()
    {
        base.Initialization();
        _inventory = Inventory.Instance;
        _decPanels = new List<DecorationButtonPanel>();
        foreach (DecorationButtonPanel panel in GridLayoutGroup.GetComponentsInChildren<DecorationButtonPanel>())
        {
            if (panel != GridLayoutGroup)
            {
                _decPanels.Add(panel);
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
        if (_inventory.decorations.Count > _decPanels.Count)
        {
            for (int i = 0; i < _inventory.decorations.Count - _decPanels.Count; i++) 
            {
                _decPanels.Add(Instantiate(_decPanels[0], _decPanels[0].transform.parent));
            }
        }
        else if (_inventory.decorations.Count < _decPanels.Count)
        {
            for (int i = 0; i < _decPanels.Count - _inventory.decorations.Count; i++)
            {
                Destroy(_decPanels[0].gameObject);
            }
            _decPanels.RemoveAll(p => p == null);
        }

        DecorationButtonPanel currPaenl;
        for (int i = 0; i < _decPanels.Count; i++)
        {
            currPaenl = _decPanels[i];
            Debug.Log("Compare " + i + " >= " + _inventory.decorations.Count);
            if (i >= _inventory.decorations.Count)
            {
                currPaenl.HidePanel();
            }
            else
            {
                Debug.Log("set picture" + currPaenl.name);
                currPaenl.ShowPanel();
                currPaenl.SetImageSprite(_inventory.decorations[i].sprite);
            }
        }
        
    }
}
