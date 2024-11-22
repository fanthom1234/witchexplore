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

    [TextAreaAttribute]
    public string OrderDescription;

}
