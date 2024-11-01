using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomButton : BaseButtonPanel
{
    public CaravanRoom.ERoom TargetRoom;

    protected override void OnClick()
    {
        base.OnClick();
        CoreGameManager.Instance.RoomController.ChangeRoom(TargetRoom);
    }
}
