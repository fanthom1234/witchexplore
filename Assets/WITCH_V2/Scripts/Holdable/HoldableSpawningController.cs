using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class HoldableSpawningController : MonoBehaviour
{
    [ReadOnly]
    public List<HoldableObject> LayerToSpawnedDecorations;

    HoldableObject _newHoldable;
    int _cahcedInt;

    private void Start()
    {
        LayerToSpawnedDecorations = new List<HoldableObject>();
    }

    /// <summary>
    /// Spawn holdable at top of order in layer, in specific layer
    /// </summary>
    /// <param name="basePrefab"></param>
    public HoldableObject SpawnHoldable(HoldableObject basePrefab, ReleaseHoldableBound releaseHoldableBound, string sortingLayerName)
    {
        _newHoldable = Instantiate(basePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity) as Decoration;
        _newHoldable.SetDecorationReleaseBound(releaseHoldableBound);
        _newHoldable.DoHold();

        if (_newHoldable as Decoration && sortingLayerName.StartsWith("Holdable - Decoration"))
        {
            LayerToSpawnedDecorations.Add(_newHoldable);
            _newHoldable.SetSortingLayer(sortingLayerName, LayerToSpawnedDecorations.Count);
            _newHoldable.name += " - " + LayerToSpawnedDecorations.Count;
        }

        return _newHoldable;
    }

    public void ShiftLayer(HoldableObject holdable, int increment)
    {
        if (holdable.HoldableRenderer.sortingLayerName.StartsWith("Holdable - Decoration") == false)
        {
            return;
        }

        for (int i = 0; i < LayerToSpawnedDecorations.Count; i++)
        {
            if (LayerToSpawnedDecorations[i] == holdable)
            {
                if (i + increment < 0 || i + increment == LayerToSpawnedDecorations.Count)
                {
                    Debug.Log("Cant ShiftLayer layer");
                    return;
                }
                SwapLayer(i, i + increment, LayerToSpawnedDecorations);
                return;
            }
        }
    }

    private void SwapLayer(int holdableOne, int holdableTwo, List<HoldableObject> layerTracker)
    {
        // cache the first
        _cahcedInt = layerTracker[holdableOne].HoldableRenderer.sortingOrder;
        _newHoldable = layerTracker[holdableOne];

        // swap their layers
        layerTracker[holdableOne].HoldableRenderer.sortingOrder = layerTracker[holdableTwo].HoldableRenderer.sortingOrder;
        layerTracker[holdableTwo].HoldableRenderer.sortingOrder = _cahcedInt;

        // swap list
        layerTracker[holdableOne] = layerTracker[holdableTwo];
        layerTracker[holdableTwo] = _newHoldable;
    }
}
