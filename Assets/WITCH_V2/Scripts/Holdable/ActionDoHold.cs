/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2024
 *	
 *	"ActionTemplate.cs"
 * 
 *	This is a blank action template.
 * 
 */

using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{
	[System.Serializable]
	public class ActionDoHold : Action
	{
		
		// Declare properties here
		public override ActionCategory Category { get { return ActionCategory.Object; }}
		public override string Title { get { return "HoldHoldable"; }}
		public override string Description { get { return "Hold"; }}

		public HoldableObject holdableObject;

		// Declare variables here
		
		
		public override float Run ()
		{
			holdableObject.DoHold();
			return 0f;
		
			
		}


		public override void Skip ()
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

			 Run ();
		}

		
		#if UNITY_EDITOR

		public override void ShowGUI ()
		{
			// Action-specific Inspector GUI code here
			holdableObject = (HoldableObject) EditorGUILayout.ObjectField("Holdable:", holdableObject, typeof (HoldableObject), true);

		}
		

		public override string SetLabel ()
		{
			// (Optional) Return a string used to describe the specific action's job.
			
			return string.Empty;
		}

		#endif
		
	}

}