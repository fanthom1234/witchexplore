using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActionCaller : MonoBehaviour
{
    protected virtual void DoActions()
    {

    }

    public void Invoke()
    {
        DoActions();
    }
}
