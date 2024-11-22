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

    protected override void Initialization()
    {
        base.Initialization();
        OrderController = OrderController.Instance;
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
}
