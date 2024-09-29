using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public UICabinet cabinet;
    public ItemData item;
    public UIShop shop;

    public void ReplacePlaceholder()
    {
        cabinet.ReplacePlaceholder(item.sprite);

        shop.itemComponent = this.gameObject;
        shop.item = this.item;
    }
}