using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;
using UnityEngine.Events;

[System.Serializable]
public class ActionScriptActions : Action
{
    public ScriptActionCaller actionsCaller;

    public override ActionCategory Category { get { return ActionCategory.Custom; } }
    public override string Title { get { return "Action from Scripts event"; } }
    public override string Description { get { return "Calls a Actions listed in ActionCaller"; } }

    public override float Run()
    {
        if (actionsCaller != null)
        {
            actionsCaller.Invoke();
        }

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
        Run();
    }


    #if UNITY_EDITOR

    public override void ShowGUI()
    {
        // Action-specific Inspector GUI code here
    }


    public override string SetLabel()
    {
        // (Optional) Return a string used to describe the specific action's job.

        return string.Empty;
    }

    #endif
}
