using AC;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [System.Serializable]
    public class BranchRuntimeData
    {
        public string Name;
        public int Level;
        public Marker[] FruitMarkers = new Marker[3];
    }

    public BranchRuntimeData[] BranchDatas;

    [Header("Object Reference")]
    public Transform FruitGroup;
    public Transform AvailableFruitGroup;

   // [Header("Asset Reference")]

    [Header("OnValidate Variable")]
    [ReadOnly]
    public int TotalLevel;

    private List<Marker> _freeMarkers;
    private Marker _tMarker;

    private void Start()
    {
        ResetUsedMarkers();
        OnDayStart();
    }

    private void ResetUsedMarkers()
    {
        _freeMarkers = new List<Marker>();
        foreach (BranchRuntimeData branchRuntimeData in BranchDatas)
        {
            for (int i = 0; i < branchRuntimeData.Level; i++)
            {
                _freeMarkers.Add(branchRuntimeData.FruitMarkers[i]);
            }
        }

        foreach (Transform child in FruitGroup)
        {
            child.SetParent(AvailableFruitGroup);
        }
    }

    private void OnDayStart()
    {
        int _fullGrowN = (int)Mathf.Ceil((float)TotalLevel / 3.0f) + 1;
        for (int i = 0; i < _fullGrowN; i++) 
        {
            GameObject fruit = AvailableFruitGroup.GetChild(0).gameObject;
            fruit.transform.SetParent(FruitGroup);
            fruit.transform.position = GetFreeBranch().Position;
        }
    }

    private Marker GetFreeBranch()
    {
        _tMarker = _freeMarkers[Random.Range(0, _freeMarkers.Count)];
        _freeMarkers.Remove( _tMarker );
        return _tMarker;
    }

    private void OnValidate()
    {
        EvaluateTotalLevel();
    }

    public void SetLevel(int branchId, int level)
    {
        BranchDatas[branchId].Level = level;
    }

    private void EvaluateTotalLevel()
    {
        TotalLevel = 0;
        foreach (BranchRuntimeData data in BranchDatas)
        {
            TotalLevel += data.Level;
        }
    }
}
