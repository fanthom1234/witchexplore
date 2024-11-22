using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class OrderController : Singleton<OrderController>
{
    [Header("Runtime Tracking")]
    public CustomerDataSO Customer;

    private List<DecorationItemSO.ETag> requiredTag;
    private Dictionary<DecorationItemSO.ETag, int> _tagToCount;

    public float EvaluateSatisfaction(BaseCakeSO cake, List<DecorationItemSO> _decorations)
    {
        float satisfaction = 5;
        CreateTagToCountDict(_decorations);

        requiredTag = new List<DecorationItemSO.ETag>(Customer.RequireTags);
        foreach (DecorationItemSO.ETag requireTag in Customer.RequireTags)
        {
            if (_tagToCount.ContainsKey(requireTag) && _tagToCount[requireTag] > 0)
            {
                requiredTag.Remove(requireTag);
            }
        }

        // If there is tag left to required, satisfaction decrease
        satisfaction -= (requiredTag.Count * 1.5f);
            
        return satisfaction;
    }

    public DecorationItemSO.ETag[] GetMissingTags()
    {
        return requiredTag.ToArray();
    }

    private void CreateTagToCountDict(List<DecorationItemSO> decorations)
    {
        _tagToCount = new Dictionary<DecorationItemSO.ETag, int>();
        foreach (DecorationItemSO item in decorations)
        {
            foreach (DecorationItemSO.ETag tag in item.Tags)
            {
                if (_tagToCount.ContainsKey(tag) == false)
                {
                    _tagToCount.Add(tag, 1);
                }
                else
                {
                    _tagToCount[tag] += 1;
                }
            }
        }
    }
}
