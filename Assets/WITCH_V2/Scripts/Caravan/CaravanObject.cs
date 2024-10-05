using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanObject : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            TryGetComponent(out _spriteRenderer);
            return _spriteRenderer;
        }
    }
    private void Awake()
    {
        Initialization();
    }

    protected virtual void Initialization()
    {

    }

    private void Update()
    {
        OnFrameStart();
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnFrameStart()
    {

    }

    private void OnEnable()
    {
        OnObjectEnabled();
    }

    protected virtual void OnObjectEnabled()
    {
    }

    private void OnDisable()
    {
        OnObjectDisable();
    }

    protected virtual void OnObjectDisable()
    {
    }

    private void OnMouseDown()
    {
        OnMouseClick();
    }

    protected virtual void OnMouseClick()
    {

    }

    private void OnMouseUp()
    {
        OnMouseRelease();
    }

    protected virtual void OnMouseRelease()
    {

    }
}
