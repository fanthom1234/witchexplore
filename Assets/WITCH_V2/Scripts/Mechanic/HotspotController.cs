using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotController : Singleton<HotspotController>
{
    private List<AC.Hotspot> hotspots;

    protected override void Awake()
    {
        base.Awake();
        hotspots = new List<AC.Hotspot>(FindObjectsByType<AC.Hotspot>(sortMode: FindObjectsSortMode.None));
    }

    public void DisableHotspots()
    {
        SetHotspotsEnabled(false);
    }

    public void EnableHotspots()
    {
        SetHotspotsEnabled(true);
    }

    public void SetHotspotsEnabled(bool e)
    {
        foreach (var hotspot in hotspots)
        {
            hotspot.enabled = e;
        }
    }
}
