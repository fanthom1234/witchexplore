using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseHoldableBound : MonoBehaviour
{
    [SerializeField] float Radius;

    public bool CheckIsObjectOnBound(Vector3 objectPos)
    {
        return Vector2.Distance(objectPos, this.transform.position) < Radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = MoreColor.Transparent(Color.blue, 0.5f);
        Gizmos.DrawSphere(transform.position, Radius);
    }
}
