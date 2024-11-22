using AC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCakeIngredient : HoldableObject, IEventSubcriber<FruitToCauldronEvent>
{
    public string FruitName;
    public FruitItemSO FruitItemSO;
    [SerializeField] float ReleaseDownForce = 7;

    private Rigidbody2D rigidbody2d;

    private Hotspot _hotspot;
    public Hotspot Hotspot
    {
        get
        {
            if(_hotspot == null)
            {
                TryGetComponent(out _hotspot);
            }
            return _hotspot;
        }
    }

    private Vector3 _startPos;
    private bool _hasFruit;

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<FruitToCauldronEvent>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<FruitToCauldronEvent>(this);
    }

    public void OnEventBusTrigger(FruitToCauldronEvent eventType)
    {
        if (eventType.FruitCakeIngredient.IsEqual(this))
        {
            Invoke("Hide", .1f);
        }
    }

    private bool IsEqual(FruitCakeIngredient fruitCakeIngredient)
    {
        return this.GetInstanceID() == fruitCakeIngredient.GetInstanceID(); ;
    }

    protected override void Initialization()
    {
        base.Initialization();
        TryGetComponent(out rigidbody2d);
        TryGetComponent(out _hotspot);
        _startPos = transform.position;
        Hide();
    }

    private void Hide()
    {
        rigidbody2d.gravityScale = 0;
        rigidbody2d.velocity = Vector2.zero;
        SpriteRenderer.enabled = false;
        transform.position = _startPos;
        Hotspot.TurnOff();
    }

    protected override void OnReleased()
    {
        base.OnReleased();
        rigidbody2d.velocity = Vector3.zero;
        rigidbody2d.gravityScale = 1;
        rigidbody2d.AddForce(Vector2.down * ReleaseDownForce, ForceMode2D.Impulse);
        StartCoroutine(DisableHotspotRoutine());
    }

    IEnumerator DisableHotspotRoutine()
    {
        Hotspot.TurnOff();
        yield return new WaitForEndOfFrame();
        Hotspot.TurnOn();
    }

    /// <summary>
    /// Call in ActionList of FruitBasket hotspot
    /// </summary>
    public void TryTurnOnHotspot()
    {
        if (_hasFruit)
        {
            Hotspot.TurnOn();
        }
        else
        {
            Hotspot.TurnOff();
        }
    }

    public void SetFruitData(FruitItemSO fruitItemSO)
    {
        if (fruitItemSO == null)
        {
            FruitName = "";
            SpriteRenderer.sprite = null;
            _hasFruit = false;
            return;
        }
        FruitName = fruitItemSO.DisplayName;
        SpriteRenderer.sprite = fruitItemSO.Sprite;
        _hasFruit = true;
        Hotspot.SetName(FruitName, Hotspot.displayLineID);
        FruitItemSO = fruitItemSO;
    }
}
