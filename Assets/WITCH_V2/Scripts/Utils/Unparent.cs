using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unparent : MonoBehaviour
{
    public Transform UnparentFrom;

    // Start is called before the first frame update
    void Awake()
    {
        if (UnparentFrom == null)
        {
            transform.parent = null;
        }
        else 
        {
            transform.parent = UnparentFrom.parent;
        }
    }
}
