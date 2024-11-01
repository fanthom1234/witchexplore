using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDecorationInteraction : Interaction
{
    public Decoration Decoration;

    protected override void OnComponentReset()
    {
        base.OnComponentReset();
        if (transform.parent != null)
        {
            transform.parent.TryGetComponent(out Decoration);
        }
    }

    public override void Interact()
    {
        base.Interact();
        // At the frame action list called
        Debug.Log("sdsd***");
        EventBus.TriggerEvent(new DecorationClickEvent(Decoration));
    }
}
