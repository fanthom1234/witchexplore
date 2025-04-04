using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DecorationUIController : CaravanObject, IEventSubcriber<RoomEnteredEvent>
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

    //public EShelfType CurrentShelf;

    [Header("Sceen Object Reference")]
    public GridLayoutGroup GridLayoutGroup;
    public UnityEngine.UI.Button FinishButton;


    private Inventory _inventory;
    private List<DecorationButtonPanel> _decPanels;
    private ReleaseHoldableBound _holdReleaseBound;
    private DecorationStation _decorationStation;

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe(this);
    }

    protected override void Initialization()
    {
        base.Initialization();
        _inventory = Inventory.Instance;
        _decPanels = new List<DecorationButtonPanel>();
        _decorationStation = FindAnyObjectByType<DecorationStation>();

        // Set Up Event of Buttons
        FinishButton.onClick.AddListener(() => InvokeFinishAndSell());

        // Get existing decoration panel
        foreach (DecorationButtonPanel panel in GridLayoutGroup.GetComponentsInChildren<DecorationButtonPanel>())
        {
            if (panel != GridLayoutGroup)
            {
                _decPanels.Add(panel);
            }
        }
        // Find appropiate are to drop decoration
        foreach (ReleaseHoldableBound bound in FindObjectsByType<ReleaseHoldableBound>(FindObjectsSortMode.None))
        {
            if (bound.name.StartsWith("Area - Decoration Place Bound"))
            {
                _holdReleaseBound = bound;
                return;
            }
        }

    }

    private void InvokeFinishAndSell()
    {
        if (_decorationStation.HasWorkingOnCake())
        {
            EventBus.TriggerEvent(new CakeSellEvent());
        }
    }

    public void OnEventBusTrigger(RoomEnteredEvent eventType)
    {
        if (eventType.ToRoom == CaravanRoom.ERoom.Decoration)
        {
            PopulateInventory();
        }
    }

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
            currPaenl.SetDecorationReleaseBound(_holdReleaseBound);
            if (i >= _inventory.decorations.Count)
            {
                currPaenl.HidePanel();
            }
            else
            {
                currPaenl.ShowPanel();
                currPaenl.SetDecoration(_inventory.decorations[i]);
            }
        }
    }
}
