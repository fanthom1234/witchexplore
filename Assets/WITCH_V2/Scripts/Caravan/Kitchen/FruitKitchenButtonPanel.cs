using System;
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
    [SerializeField] FruitCakeIngredient HoldableFruit;

    private Inventory.InvenItem fruit;

    public void SetFruit(Inventory.InvenItem fruitItem)
    {
        fruit = fruitItem;
        EvaluateAmountText();
        FruitImage.sprite = fruitItem.ItemData.Sprite;
        HoldableFruit.SpriteRenderer.sprite = FruitImage.sprite;
    }

    protected override void OnClick()
    {
        base.OnClick();
        HoldableFruit.transform.position = this.transform.position;
        HoldableFruit.SetFruitData(fruit.ItemData as FruitItemSO);
        HoldableFruit.DoHold();
        fruit.Count--;
        if (fruit.Count == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            EvaluateAmountText();
        }
    }

    private void EvaluateAmountText()
    {
        if (fruit.Count > 1)
        {
            AmountText.text = "x" + fruit.Count.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }
}
