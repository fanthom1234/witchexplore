using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookUIGroup : BaseUIPanel, IEventSubcriber<CakeSellEvent>
{
    [Header("Debugging")]
    public bool ForceShow = false;

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<CakeSellEvent>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusSubcribe<CakeSellEvent>(this);
    }

    public void OnEventBusTrigger(CakeSellEvent eventType)
    {
        StartCoroutine(DelayShowRoutine());
    }

    IEnumerator DelayShowRoutine()
    {
        yield return new WaitForSeconds(1);
        ShowPanel();
        yield return new WaitForSeconds(1);
        HidePanel();
    }

    protected override void OnInspectorChanged()
    {
        base.OnInspectorChanged();
        if (ForceShow)
        {
            InstantSetAlpha(1);
        }
        else
        {
            InstantSetAlpha(0);
        }
    }
}
