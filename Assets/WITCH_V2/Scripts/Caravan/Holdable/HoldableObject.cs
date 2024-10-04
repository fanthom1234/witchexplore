using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HoldableObject : CaravanObject
{
    public void DoRelease()
    {
        OnReleased();
    }

    protected virtual void OnReleased()
    {

    }
}
