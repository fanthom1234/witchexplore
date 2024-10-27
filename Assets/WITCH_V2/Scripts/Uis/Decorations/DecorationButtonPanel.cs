using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DecorationButtonPanel : BaseButtonPanel
{
    [SerializeField] Image DecoraionImage;
    [SerializeField] Decoration HoldDecorationPrefab;

    private Decoration _currHold;
    private ReleaseHoldableBound _releaseBound;

    protected override void Initialization()
    {
        base.Initialization();
    }

    public void SetImageSprite(Sprite sprite)
    {
        DecoraionImage.sprite = sprite;
    }

    protected override void OnClick()
    {
        base.OnClick();
        _currHold = Instantiate(HoldDecorationPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        _currHold.SetDecorationReleaseBound(_releaseBound);
        _currHold.SetDecorationSprite(DecoraionImage.sprite);
        _currHold.DoHold();
    }

    public void SetDecorationReleaseBound(ReleaseHoldableBound holdReleaseBound)
    {
        _releaseBound = holdReleaseBound;
    }
}
