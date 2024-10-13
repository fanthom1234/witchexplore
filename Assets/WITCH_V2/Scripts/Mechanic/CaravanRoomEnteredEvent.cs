using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanRoom
{
    public enum ERoom
    {
        NoneSpecific,
        Kitchen,
        Decoration,
        MainRoom,
    }
}

public struct RoomEnteredEvent 
{
    public CaravanRoom.ERoom ToRoom;
    public CaravanRoom.ERoom FromRoom;

    public RoomEnteredEvent(CaravanRoom.ERoom to, CaravanRoom.ERoom from)
    {
        ToRoom = to;
        FromRoom = from;
    }
}
