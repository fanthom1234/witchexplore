using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorationAdjustmentPanel : BaseUIPanel, IEventSubcriber<DecorationClickEvent>
{
    public Button[] Buttons;

    private Decoration _target;

    private enum EAdjustment
    {
        Rotate,
        RotateR,
        Flip,
        Duplicate,
        BringFront,
        BringBack,
        Delete,
        Move,
    }

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<DecorationClickEvent>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<DecorationClickEvent>(this);
    }

    protected override void Initialization()
    {
        base.Initialization();
        Buttons[(int)EAdjustment.Rotate].onClick.AddListener(() => _target?.Rotate(1));
        Buttons[(int)EAdjustment.RotateR].onClick.AddListener(() => _target?.Rotate(-1));
        
        Buttons[(int)EAdjustment.Flip].onClick.AddListener(() => _target?.Flip());
        //Buttons[(int)EAdjustment.Duplicate].onClick.AddListener());

        Buttons[(int)EAdjustment.BringFront].onClick.AddListener(() => _target?.ShiftLayer(1));
        Buttons[(int)EAdjustment.BringBack].onClick.AddListener(() => _target?.ShiftLayer(-1));

        Buttons[(int)EAdjustment.Delete].onClick.AddListener(() => HidePanel());

        Buttons[(int)EAdjustment.Move].onClick.AddListener(() => _target?.DoHold()); 
        Buttons[(int)EAdjustment.Move].onClick.AddListener(() => HidePanel()); 
    }

    public void AdjustFlipHorizontal()
    {
        _target?.Flip();
    }

    public void OnEventBusTrigger(DecorationClickEvent eventType)
    {
        if (eventType.Decoration == null)
        {
            _target = null;
        }
        _target = eventType.Decoration;
        ShowPanel();
    }
}
