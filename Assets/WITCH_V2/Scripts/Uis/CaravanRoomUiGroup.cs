using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanRoomUiGroup : BaseUIPanel, IEventSubcriber<RoomEnteredEvent>
{
    public CaravanRoom.ERoom ThisRoom;

    private List<RectTransform> _items; 

    [Header("Debugging")]
    public bool ForceShow = false;

    protected override void OnObjectEnabled()
    {
        base.OnObjectEnabled();
        EventBusRegister.EventBusSubcribe<RoomEnteredEvent>(this);
    }

    protected override void OnObjectDisable()
    {
        base.OnObjectDisable();
        EventBusRegister.EventBusSubcribe<RoomEnteredEvent>(this);
    }

    public void OnEventBusTrigger(RoomEnteredEvent eventType)
    {
        if (eventType.ToRoom == ThisRoom)
        {
            ShowPanel();

        }
        if (eventType.FromRoom == ThisRoom)
        {
            HidePanel();
        }
    }

    protected override void OnInspectorChanged()
    {
        base.OnInspectorChanged();
        if (ForceShow)
        {
            InstantSetAlpha(1);
        }
        else
        {
            InstantSetAlpha(0);
        }
    }

   
}
