using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButtonPanel : BaseUIPanel
{
    private Button _button;
    public Button Button { get { return _button; } }

    protected override void Initialization()
    {
        base.Initialization();
        TryGetComponent(out _button);
        Button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {

    }

}
