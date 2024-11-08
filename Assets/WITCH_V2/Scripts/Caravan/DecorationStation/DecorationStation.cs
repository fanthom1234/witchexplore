using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CakeSellEvent
{
    
}

public class DecorationStation : CaravanObject, IEventSubcriber<CakeCraftedEvent>, IEventSubcriber<CakeSellEvent>
{
    [Header("Object Reference")]
    [SerializeField] SpriteRenderer WorkingOnCake;

    // Assign on Init
    private HoldableSpawningController HoldableSpawningController;

    protected override void Initialization()
    {
        base.Initialization();
        WorkingOnCake.enabled = false;
        HoldableSpawningController = CoreGameManager.Instance.HoldableSpawningController;
    }

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<CakeCraftedEvent>(this);
        EventBusRegister.EventBusSubcribe<CakeSellEvent>(this);
    }
    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<CakeCraftedEvent>(this);
        EventBusRegister.EventBusUnscribe<CakeSellEvent>(this);
    }

    public void OnEventBusTrigger(CakeCraftedEvent eventType)
    {
        WorkingOnCake.enabled = true;
        WorkingOnCake.sprite = eventType.Result.CakeSprite;
    }

    public void OnEventBusTrigger(CakeSellEvent eventType)
    {
        // TODO add anim before
        WorkingOnCake.enabled = false;
        HoldableSpawningController.ClearDecorations();
    }
}
