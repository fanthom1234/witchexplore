using AC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CaravanRoom;

public class RoomController : MonoBehaviour, IEventSubcriber<RoomEnteredEvent>
{
    public Transform CameraTransform;
    public Marker[] RoomToCamPosMaker;

    private ERoom currRoom = 0;

    public void ChangeRoom(ERoom to, ERoom from)
    {
        currRoom = from;
        ChangeRoom(to);
    }

    private void OnEnable()
    {
        EventBusRegister.EventBusSubcribe<RoomEnteredEvent>(this);
    }

    private void OnDisable()
    {
        EventBusRegister.EventBusUnscribe<RoomEnteredEvent>(this);
    }

    public void ChangeRoom(ERoom to)
    {
        CameraTransform.transform.position = RoomToCamPosMaker[(int)to].Position;
        EventBus.TriggerEvent(new RoomEnteredEvent(to, currRoom));
    }

    public void OnEventBusTrigger(RoomEnteredEvent eventType)
    {
        currRoom = eventType.ToRoom;
    }
}
