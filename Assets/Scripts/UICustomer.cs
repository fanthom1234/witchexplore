using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class UICustomer : MonoBehaviour
{
    public CustomerData customerData;

    public UnityEngine.UI.Image image;

    private void Start() {
        image.sprite = customerData.customerSprite;
    }
}
