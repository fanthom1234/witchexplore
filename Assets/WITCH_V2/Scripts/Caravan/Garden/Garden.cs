using AC;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Garden : CaravanObject, IEventSubcriber<WateringEvent>
{
    [System.Serializable]
    public class BranchRuntimeData
    {
        public string Name;
        public FruitItemSO.EFruitType FruitType;
        public int Level;
        public Marker[] FruitMarkers = new Marker[3];
    }

    public BranchRuntimeData[] BranchDatas;

    [Header("Asset Reference")]
    public FruitItemSO[] PossibleFruit;

    [Header("Object Reference")]
    public Transform FruitGroup;
    public Transform AvailableFruitGroup;

    [Header("OnValidate Variable")]
    [ReadOnly]
    public int TotalLevel;
    public int GrowOnWatering;

    private List<Marker> _freeMarkers;
    private Marker _tMarker;
    private int _fruitGrownCount = 0;


    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<WateringEvent>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusUnscribe<WateringEvent>(this);
    }

    protected override void Initialization()
    {
        base.Initialization();
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
            GrowFruit();
        }
        _fruitGrownCount = 0;
    }

    private void GrowFruit()
    {
        FruitItemSO fruitItem = GetFruitInNeeded();
        Marker m = GetFreeBranch();
        if (m == null)
        {
            Debug.Log("No Free Branch");
            return;
        }
        // If there is no branch space left
        if (AvailableFruitGroup.childCount <= 0)
        {
            return;
        }

        GardenFruit _fruit;
        if (AvailableFruitGroup.GetChild(0).gameObject.TryGetComponent(out _fruit))
        {
            _fruit.transform.SetParent(FruitGroup);
            _fruit.transform.position = m.Position;
            _fruit.SetFruit(fruitItem);
            _fruit.SetStage(GardenFruit.EStage.FullyGrown);
        }
    }

    private FruitItemSO GetFruitInNeeded()
    {
        return PossibleFruit[Random.Range(0, PossibleFruit.Length)];
    }

    private Marker GetFreeBranch()
    {
        if (_freeMarkers.Count <= 0)
        {
            return null;
        }
        _tMarker = _freeMarkers[Random.Range(0, _freeMarkers.Count)];
        _freeMarkers.Remove(_tMarker);
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
        GrowOnWatering = (int)Mathf.Floor((float)TotalLevel / 3.0f); 
    }

    public void OnEventBusTrigger(WateringEvent eventType)
    {
        for (int i = 0; i < GrowOnWatering; i++)
        {
            GrowFruit();
        }
    }
}
