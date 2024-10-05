using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeIngredientGrabber : ScriptActionCaller
{
    [SerializeField] HoldableObject HoldableObject;


    protected override void DoActions()
    {
        base.DoActions();
        HoldableObject.DoHold();
    }
}
