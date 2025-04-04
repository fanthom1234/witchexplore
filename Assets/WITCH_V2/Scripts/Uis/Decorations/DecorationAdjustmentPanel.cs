using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class DecorationAdjustmentPanel : BaseUIPanel, IEventSubcriber<DecorationClickEvent>
{
    [Header("Reference")]
    public Vector2 OffsetFromDecoration;
    [Header("Reference")]
    public Button[] Buttons;

    private Decoration _target;

    private enum EAdjustment
    {
        Move,
        Rotate,
        RotateR,
        Flip,
        Duplicate,
        BringBack,
        BringFront,
        Delete,
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

        Buttons[(int)EAdjustment.Duplicate].onClick.AddListener(() => _target?.Duplicate());
        Buttons[(int)EAdjustment.Duplicate].onClick.AddListener(() => HidePanel());

        Buttons[(int)EAdjustment.BringFront].onClick.AddListener(() => _target?.ShiftLayer(1));
        Buttons[(int)EAdjustment.BringBack].onClick.AddListener(() => _target?.ShiftLayer(-1));

        Buttons[(int)EAdjustment.Delete].onClick.AddListener(() => _target?.Delete());
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
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _target.transform.position);
        RectTransform.anchoredPosition = (Vector2)_target.transform.InverseTransformPoint(screenPoint) + OffsetFromDecoration;
        ShowPanel();
    }
}
