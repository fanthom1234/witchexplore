using AC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerMirror : MonoBehaviour
{
    public SpriteFader CustomerFader;
    public float FadeDuration = 1;
    public BaseUIPanel CustomerOrderPanel;
    public TextMeshProUGUI OrderText;

    private HotspotController HotspotController;
    private OrderController OrderController;
    private SpriteRenderer _customerRenderer;

    private void Start()
    {
        HotspotController = HotspotController.Instance;
        OrderController = OrderController.Instance;
        CustomerFader.TryGetComponent(out _customerRenderer);
    }

    /// <summary>
    /// Call Event from "Hotspot2D_ReceiveOrder"
    /// </summary>
    public void ShowCustomer()
    {
        CustomerFader.Fade(FadeType.fadeIn, FadeDuration);
        CustomerOrderPanel.ShowPanel();
        HotspotController.DisableHotspots();

        OrderText.text = OrderController.Customer.OrderDescription;
        _customerRenderer.sprite = OrderController.Customer.Sprite;
    }

    /// <summary>
    /// Call from Button Event of "Button - Order Okay"
    /// </summary>
    public void HideCustomer()
    {
        CustomerFader.Fade(FadeType.fadeOut, FadeDuration);
        CustomerOrderPanel.HidePanel();
        HotspotController.EnableHotspots();
    }
}
