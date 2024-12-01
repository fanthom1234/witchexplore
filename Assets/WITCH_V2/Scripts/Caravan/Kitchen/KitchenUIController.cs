using UnityEngine;
using UnityEngine.UI;

public class KitchenUIController : CaravanObject, IEventSubcriber<CakeCraftedEvent>
{
    [Header("Object Reference")]
    public Image ResultCakeImage;
    public CanvasGroup SuccessGroup;
    public CanvasGroup FailGroup;
    public CaravanRoomUiGroup RoomUi;

    HotspotController HotspotController => HotspotController.Instance;


    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<CakeCraftedEvent>(this);
        BaseUIPanel.HideCanvasGroup(SuccessGroup);
        BaseUIPanel.HideCanvasGroup(FailGroup);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<CakeCraftedEvent>(this);
    }

    public void OnEventBusTrigger(CakeCraftedEvent eventType)
    {
        HotspotController.DisableHotspots();
        if (eventType.Result == null)
        {
            BaseUIPanel.HideCanvasGroup(SuccessGroup);
            BaseUIPanel.ShowCanvasGroup(FailGroup);
        }
        else
        {
            BaseUIPanel.HideCanvasGroup(FailGroup);
            BaseUIPanel.ShowCanvasGroup(SuccessGroup);
            ResultCakeImage.sprite = eventType.Result.CakeSprite;
        }
    }

    public void CloseResultUI()
    {
        HotspotController.EnableHotspots();
        BaseUIPanel.HideCanvasGroup(SuccessGroup);
        BaseUIPanel.HideCanvasGroup(FailGroup);
    }

    public void GoToDecoration()
    {

        CloseResultUI();
    }
}
