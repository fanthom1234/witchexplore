using AC;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static DecorationItemSO;

[CreateAssetMenu(fileName = "Cust_", menuName = "WB/Customer")]
public class CustomerDataSO : ItemData
{
    public string CustomerName;

    // -2 Penalty for missing, +1 reward for inclusion
    public DecorationItemSO.ETag[] RequireTags;
    // +1 reward for inclusion per tag not piece
    public DecorationItemSO.ETag HiddenLikeTags;

    //[ReadOnly]
    //public string TagsString;

    //private void OnValidate()
    //{
    //    TagsString = GenerateString();
    //}

    //public string GenerateString()
    //{
    //    string s = "";
    //    foreach (ETag tag in RequireTags)
    //    {
    //        s += tag.ToString();
    //    }
    //    return s;
    //}
}
