using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

    [System.Serializable]
    public class ActionTriggerRoomChangedEvent : Action
    {

        // Declare properties here
        public override ActionCategory Category { get { return ActionCategory.Custom; } }
        public override string Title { get { return "TriggerRoomChangedEvent"; } }
        public override string Description { get { return "TriggerRoomChangedEvent"; } }


        // Declare variables here
        public CaravanRoom.ERoom ToRoom;
        public CaravanRoom.ERoom FromRoom;

        public override float Run()
        {
            EventBus.TriggerEvent(new RoomEnteredEvent(ToRoom, FromRoom));
            /* 
			 * This function is called when the action is performed.
			 * 
			 * The float to return is the time that the game
			 * should wait before moving on to the next action.
			 * Return 0f to make the action instantenous.
			 * 
			 * For actions that take longer than one frame,
			 * you can return "defaultPauseTime" to make the game
			 * re-run this function a short time later. You can
			 * use the isRunning boolean to check if the action is
			 * being run for the first time, eg: 
			 */

            if (!isRunning)
            {
                isRunning = true;
                return defaultPauseTime;
            }
            else
            {
                isRunning = false;
                return 0f;
            }
        }


        public override void Skip()
        {
            /*
			 * This function is called when the Action is skipped, as a
			 * result of the player invoking the "EndCutscene" input.
			 * 
			 * It should perform the instructions of the Action instantly -
			 * regardless of whether or not the Action itself has been run
			 * normally yet.  If this method is left blank, then skipping
			 * the Action will have no effect.  If this method is removed,
			 * or if the Run() method call is left below, then skipping the
			 * Action will cause it to run itself as normal.
			 */

            Run();
        }


#if UNITY_EDITOR

        public override void ShowGUI()
        {
            // Action-specific Inspector GUI code here
            ToRoom = (CaravanRoom.ERoom) EditorGUILayout.EnumPopup("Entering Room", ToRoom);
            FromRoom = (CaravanRoom.ERoom) EditorGUILayout.EnumPopup("Exiting Room", FromRoom);
        }


        public override string SetLabel()
        {
            // (Optional) Return a string used to describe the specific action's job.

            return string.Empty;
        }

#endif

    }

}
