using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DecorationButtonPanel : BaseButtonPanel
{
    [SerializeField] Image DecoraionImage;
    [SerializeField] Decoration HoldDecorationPrefab;
    
    private PlayerHoldingController _playerHoldingController;
    private HoldableSpawningController HoldableSpawningController;

    private Decoration _currHold;
    private DecorationItemSO _decorationItem;
    private ReleaseHoldableBound _releaseBound;

    protected override void Initialization()
    {
        base.Initialization();
        _playerHoldingController = CoreGameManager.Instance.PlayerHoldingController;
        HoldableSpawningController = CoreGameManager.Instance.HoldableSpawningController;
    }

    public void SetDecoration(DecorationItemSO decoration)
    {
        _decorationItem = decoration;
        DecoraionImage.sprite = decoration.Sprite;
    }

    protected override void OnClick()
    {
        base.OnClick();
        _playerHoldingController.TryDestroyHolding();
        _currHold = HoldableSpawningController.SpawnHoldable(HoldDecorationPrefab, _releaseBound, "Holdable - Decoration") as Decoration;
        _currHold.SetDecorationSprite(_decorationItem.Sprite);
        _currHold.SetDecorationInteracableSize(Vector2.one *  _decorationItem.Size);
    }

    public void SetDecorationReleaseBound(ReleaseHoldableBound holdReleaseBound)
    {
        _releaseBound = holdReleaseBound;
    }
}
