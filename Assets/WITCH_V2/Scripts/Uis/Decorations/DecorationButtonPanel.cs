using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorationButtonPanel : BaseUIPanel
{
    [SerializeField] Image DecoraionImage;

    public void SetImageSprite(Sprite sprite)
    {
        DecoraionImage.sprite = sprite;
    }
}
