using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameManager : Singleton<CoreGameManager>
{
    public PlayerHoldingController PlayerHoldingController;
    public HoldableSpawningController HoldableSpawningController;
    public DecorationUIController DecorationStationController;
    public RoomController RoomController;
    public OrderController OrderController;
}
