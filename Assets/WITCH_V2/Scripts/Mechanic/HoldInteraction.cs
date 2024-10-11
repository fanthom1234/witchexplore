using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInteraction : Interaction
{
    [SerializeField] HoldableObject HoldableObject;

    protected override void OnComponentReset()
    {
        base.OnComponentReset();
        if (transform.parent != null) 
        {
            transform.parent.TryGetComponent(out HoldableObject);
        }
    }

    public override void Interact()
    {
        base.Interact();
        // At the frame action list called

        HoldableObject.DoHold();
    }
}
