using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotController : Singleton<HotspotController>, IEventSubcriber<ObjectHoldEvent>
{
    private List<AC.Hotspot> hotspots;

    protected override void Awake()
    {
        base.Awake();
        hotspots = new List<AC.Hotspot>(FindObjectsByType<AC.Hotspot>(sortMode: FindObjectsSortMode.None));
    }

    private void OnEnable()
    {
        EventBusRegister.EventBusSubcribe<ObjectHoldEvent>(this);
    }

    private void OnDisable()
    {
        EventBusRegister.EventBusUnscribe<ObjectHoldEvent>(this);
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

    public void OnEventBusTrigger(ObjectHoldEvent eventType)
    {
        if (eventType.Holdable == null)
        {
            EnableHotspots();
        }
        else
        {
            DisableHotspots();
        }
    }
}
