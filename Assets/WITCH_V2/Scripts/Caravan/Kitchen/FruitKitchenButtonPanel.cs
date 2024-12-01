using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FruitKitchenButtonPanel : BaseButtonPanel
{
    [SerializeField] Image FruitImage;
    [SerializeField] TextMeshProUGUI AmountText;

    public void SetFruit(Inventory.InvenItem fruitItem)
    {
        if (fruitItem.Count > 1)
        {
            AmountText.text = "x" + fruitItem.Count.ToString();
        }
        else
        {
            AmountText.text = "";
        }
        FruitImage.sprite = fruitItem.ItemData.Sprite;
    }
}
