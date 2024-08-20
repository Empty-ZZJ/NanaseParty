using UnityEngine;
using System.Collections;

namespace CrazyMinnow.SALSA.Examples
{
	/// <summary>
	/// A simple class to provide a GUI reset button that calls 
	/// the reset functions on the CM_DialogSystem and CM_SalsaWaypoints
	/// in the demo scene.
	/// </summary>
	public class CM_GameManager : MonoBehaviour 
	{
		public CM_DialogSystem dialogSystem;
		public CM_SalsaWaypoints spiderWaypoints;
		public CM_SalsaWaypoints boxheadWaypoints;

		/// <summary>
		/// GUI Reset button, resets the dialog sequence in the demo scene
		/// </summary>
		void OnGUI () 
		{
			if (GUI.Button(new Rect(20, Screen.height - 55, 75, 35), "Reset"))
			{
				spiderWaypoints.ResetSalsaWaypoints();
				boxheadWaypoints.ResetSalsaWaypoints();
				dialogSystem.ResetDialog();
			}
		}
	}
}