using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookUIGroup : BaseUIPanel, IEventSubcriber<CakeSellEvent>, IEventSubcriber<SatisfactionEvaluatedEvent>
{
    [Header("Object Reference")]
    public TextMeshProUGUI SatisfactionNumberText;
    public TextMeshProUGUI CommentText;

    [Header("Debugging")]
    public bool ForceShow = false;

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<CakeSellEvent>(this);
        EventBusRegister.EventBusSubcribe<SatisfactionEvaluatedEvent>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusSubcribe<CakeSellEvent>(this);
        EventBusRegister.EventBusSubcribe<SatisfactionEvaluatedEvent>(this);
    }

    public void OnEventBusTrigger(CakeSellEvent eventType)
    {
        StartCoroutine(DelayShowRoutine());
    }

    IEnumerator DelayShowRoutine()
    {
        yield return new WaitForSeconds(1);
        ShowPanel();
        yield return new WaitForSeconds(3);
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

    public void OnEventBusTrigger(SatisfactionEvaluatedEvent eventType)
    {
        SatisfactionNumberText.text = eventType.Satisfaction.ToString() + "/10";

        CommentText.text = "";
        foreach (DecorationItemSO.ETag tag in eventType.MissingTags)
        {
            CommentText.text += tag.ToString() + " is missing.\n";
        }
    }
}
