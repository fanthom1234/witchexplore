using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMirror : MonoBehaviour
{
    public SpriteFader CustomerFader;
    public float FadeDuration = 1;
    public BaseUIPanel CustomerOrderPanel;

    private HotspotController HotspotController;

    private void Start()
    {
        HotspotController = HotspotController.Instance;
    }

    /// <summary>
    /// Call Event from "Hotspot2D_ReceiveOrder"
    /// </summary>
    public void ShowCustomer()
    {
        CustomerFader.Fade(FadeType.fadeIn, FadeDuration);
        CustomerOrderPanel.ShowPanel();
        HotspotController.DisableHotspots();
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
