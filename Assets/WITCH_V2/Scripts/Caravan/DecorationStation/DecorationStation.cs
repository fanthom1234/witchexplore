using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CakeSellEvent
{
    
}
public struct SatisfactionEvaluatedEvent
{
    public float Satisfaction;
    public DecorationItemSO.ETag[] MissingTags;

    public SatisfactionEvaluatedEvent(float satisfaction, DecorationItemSO.ETag[] missingTags)
    {
        Satisfaction = satisfaction;
        MissingTags = missingTags;
    }
}

public class DecorationStation : CaravanObject, IEventSubcriber<CakeCraftedEvent>, IEventSubcriber<CakeSellEvent>, IEventSubcriber<DecorationPlacedEvent>
{
    [Header("Object Reference")]
    [SerializeField] SpriteRenderer WorkingOnCake;

    private BaseCakeSO _workingCake;
    private List<DecorationItemSO> _decorations;

    // Assign on Init
    private HoldableSpawningController HoldableSpawningController;
    private OrderController OrderController;

    protected override void Initialization()
    {
        base.Initialization();
        _decorations = new List<DecorationItemSO>();
        WorkingOnCake.enabled = false;
        HoldableSpawningController = CoreGameManager.Instance.HoldableSpawningController;
        OrderController = CoreGameManager.Instance.OrderController;
    }

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<CakeCraftedEvent>(this);
        EventBusRegister.EventBusSubcribe<CakeSellEvent>(this);
        EventBusRegister.EventBusSubcribe<DecorationPlacedEvent>(this);
    }
    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<CakeCraftedEvent>(this);
        EventBusRegister.EventBusUnscribe<CakeSellEvent>(this);
        EventBusRegister.EventBusUnscribe<DecorationPlacedEvent>(this);
    }

    public void OnEventBusTrigger(CakeCraftedEvent eventType)
    {
        if (eventType.Result)
        {
            _workingCake = eventType.Result;
            WorkingOnCake.enabled = true;
            WorkingOnCake.sprite = eventType.Result.CakeSprite;
        }
    }

    public void OnEventBusTrigger(CakeSellEvent eventType)
    {
        float satis = OrderController.EvaluateSatisfaction(_workingCake, _decorations);
        EventBus.TriggerEvent(new SatisfactionEvaluatedEvent(satis, OrderController.GetMissingTags()));

        _decorations.Clear();
        // TODO add anim before
        WorkingOnCake.enabled = false;
        HoldableSpawningController.ClearDecorations();
    }

    public bool HasWorkingOnCake()
    {
        return WorkingOnCake.enabled;
    }

    public void OnEventBusTrigger(DecorationPlacedEvent eventType)
    {
        _decorations.Add(eventType.Decoration.DecorationData);
    }
}
