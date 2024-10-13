﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIPanel : CaravanObject
{
    public bool InstantHideOnInitial = false;
    public bool InstantAlphaOnEnable = true;

    public bool IsShowing { get; protected set; }

    #region PROPERTY
    private CanvasGroup _canvasGroup;
    public CanvasGroup CanvasGroup
    {
        get
        {
            if (_canvasGroup == null)
            {
                TryGetComponent(out _canvasGroup);
            }
            return _canvasGroup;
        }
    }

    private RectTransform _rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                TryGetComponent(out _rectTransform);
            }
            return _rectTransform;
        }
    }
    #endregion

    #region OnEnable & OnDisable
    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        OnPanelEnable();
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        OnPanelDisable();
    }

    protected virtual void OnPanelEnable()
    {
        
    }

    protected virtual void OnPanelDisable()
    {
        
    }
    #endregion

    protected override void Initialization()
    {
        base.Initialization();
        if (InstantHideOnInitial)
        {
            IsShowing = false;
            InstantSetAlpha(0);
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (IsShowing)
        {
            ProcessPanel();
        }
    }

    protected virtual void ProcessPanel() { }

    public void ShowPanel()
    {
        IsShowing = true;
        OnShowingPanel();
        OnVisibleChanged(IsShowing);
    }

    public void HidePanel()
    {
        IsShowing = false;
        OnHidingPanel();
        OnVisibleChanged(IsShowing);
    }

    protected void InstantSetAlpha(float alpha)
    {
        CanvasGroup.alpha = alpha;
    }

    public void ShowPanelSilent()
    {
        IsShowing = true;
    }

    public void HidePanelSilent()
    {
        IsShowing = false;
    }

    protected virtual void OnShowingPanel()
    {
        if (InstantAlphaOnEnable)
        {
            InstantSetAlpha(1);
        }
    }

    protected virtual void OnHidingPanel()
    {
        if (InstantAlphaOnEnable)
        {
            InstantSetAlpha(0);
        }
    }

    protected virtual void OnVisibleChanged(bool currentVisibility)
    {

    }

    public void SetShowPanel(bool isShow)
    {
        if (isShow)
        {
            ShowPanel();
        }
        else
        {
            HidePanel();
        }
    }
}
