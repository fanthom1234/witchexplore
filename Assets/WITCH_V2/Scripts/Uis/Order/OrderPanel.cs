using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderPanel : BaseUIPanel
{
    [SerializeField] Image CustomerFaceImage;
    [SerializeField] TextMeshProUGUI OrderText;
    OrderController OrderController;
    HotspotController HotspotController;

    protected override void Initialization()
    {
        base.Initialization();
        OrderController = OrderController.Instance;
        HotspotController = HotspotController.Instance;
    }

    protected override void OnVisibleChanged(bool currentVisibility)
    {
        base.OnVisibleChanged(currentVisibility);
        if (currentVisibility)
        {
            OrderText.text = OrderController.Customer.OrderDescription;
            CustomerFaceImage.sprite = OrderController.Customer.Sprite;
        }
    }

    protected override void OnShowingPanel()
    {
        base.OnShowingPanel();
        HotspotController.DisableHotspots();
    }

    protected override void OnHidingPanel()
    {
        base.OnHidingPanel();
        HotspotController.EnableHotspots();
    }
}
