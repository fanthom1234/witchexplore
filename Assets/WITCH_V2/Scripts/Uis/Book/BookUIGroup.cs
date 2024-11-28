using System.Collections;
using TMPro;
using UnityEngine;

public class BookUIGroup : BaseUIPanel, IEventSubcriber<CakeSellEvent>, IEventSubcriber<SatisfactionEvaluatedEvent>
{
    [SerializeField]
    BaseUIPanel[] PagePanels;

    [Header("Object Reference")]
    public RectTransform SatisfactionMask;
    public TextMeshProUGUI CommentText;

    [Header("Debugging")]
    public bool ForceShow = false;

    private int _currPage = -1;
    public const int RECIPE = 0;
    public const int COLLECTION = 1;
    public const int SUMMARY = 2;
    private HotspotController _hotspotController;

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

    protected override void Initialization()
    {
        base.Initialization();
        _hotspotController = HotspotController.Instance;
    }

    protected override void OnShowingPanel()
    {
        base.OnShowingPanel();
        _hotspotController.DisableHotspots();
    }

    protected override void OnHidingPanel()
    {
        base.OnHidingPanel();
        _hotspotController.EnableHotspots();
    }

    public void OnEventBusTrigger(CakeSellEvent eventType)
    {
        SetPage(SUMMARY);
        StartCoroutine(DelayShowRoutine());
    }

    public void SetPage(int targetPage)
    {
        if (_currPage >= 0)
        {
            PagePanels[_currPage].HidePanel();
        }
        _currPage = targetPage;
        PagePanels[targetPage].ShowPanel();
    }

    IEnumerator DelayShowRoutine()
    {
        yield return new WaitForSeconds(.25f);
        ShowPanel();
        yield return new WaitForSeconds(5);
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
        float w = (70 * Mathf.Floor(eventType.Satisfaction)) - 20;
        if (eventType.Satisfaction > Mathf.Floor(eventType.Satisfaction))
        {
            w -= 25;
        }
        Debug.Log(w + " " + eventType.Satisfaction);
        SatisfactionMask.sizeDelta = new Vector2(w, 50);
        //SatisfactionNumberText.text = eventType.Satisfaction.ToString() + "/10";

        CommentText.text = "";
        foreach (DecorationItemSO.ETag tag in eventType.MissingTags)
        {
            CommentText.text += tag.ToString() + " is missing.\n";
        }
    }
}


