using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationStation : CaravanObject, IEventSubcriber<CakeCraftedEvent>
{
    [Header("Object Reference")]
    [SerializeField] SpriteRenderer WorkingOnCake;

    protected override void Initialization()
    {
        base.Initialization();
        WorkingOnCake.enabled = false;
    }

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<CakeCraftedEvent>(this);
    }
    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<CakeCraftedEvent>(this);
    }

    public void OnEventBusTrigger(CakeCraftedEvent eventType)
    {
        WorkingOnCake.enabled = true;
        WorkingOnCake.sprite = eventType.Result.CakeSprite;
    }
}
