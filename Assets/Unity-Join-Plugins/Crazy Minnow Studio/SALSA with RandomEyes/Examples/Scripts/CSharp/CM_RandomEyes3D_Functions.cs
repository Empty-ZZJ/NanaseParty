using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

namespace CrazyMinnow.SALSA.Examples
{
	/// <summary>
	/// Demonstrates use of the RandomEyes3D public methods
	/// </summary>
	public class CM_RandomEyes3D_Functions : MonoBehaviour 
	{
		public RandomEyes3D randomEyes3D; // Reference to the RandomEyes3D class
		public Camera mainCam; // Game camera
		public SpriteRenderer target; // SpriteRenderer target used to demonstrate eye target tracking
		
		private bool random = true; // Toggle [Random Eyes] checkbox on the RandomEyes inspector
		private bool affinity = false; // Toggle [Target Affinity] for the mainCam
		private bool affinitySet = true; // Used to make sure SetTargetAffinity only fire once
		private bool track = false; // Set/clear the [Look Target] on the RandomEyes inspector
		private bool trackSet = true; // Used to make sure SetLookTarget only fires once
		private Vector3 targetPosHome; // Home position
		private Vector3 targetPos; // Target sprite mapped to cursor position to demonstrate [Look Target] eye tracking
		
		// These private variables are used to position buttons in the OnGUI method
		private int xPos = 0; // The Z position of a GUI button
		private int yPos = 0; // The Y position of a GUI button
		private int yGap = 5; // The vertical spacing between GUI buttons
		private int xWidth = 150; // The X width of GUI buttons
		private int yHeight = 30; // The X width of GUI buttons
		
		/// <summary>
		/// On start, try to get a local reference to the RandomEyes3D class and the scene  camera
		/// </summary>
		void Start()
		{
			if (!randomEyes3D) // randomEyes3D is null
				randomEyes3D = (RandomEyes3D)FindObjectOfType(typeof(RandomEyes3D)); // Try to get a local reference to RandomEyes3D
			
			if (!mainCam) // mainCam is null
				mainCam = (Camera)FindObjectOfType(typeof(Camera)); // Try to get a local reference to the scene camera
			
			targetPosHome = target.transform.position;
		}
		
		/// <summary>
		/// Draw the GUI buttons
		/// </summary>
		void OnGUI()
		{
			xPos = Screen.width - 20 - xWidth; // X position for right side GUI controls
			yPos = 0; // Reset the button Y position
			
			#region Turn random blink on or off
			yPos += yGap;
			if (GUI.Button (new Rect (xPos, yPos, xWidth, yHeight), "Toggle Blink")) 
			{
				if (randomEyes3D.randomBlink) 
				{
					randomEyes3D.SetBlink (false);
				} 
				else 
				{
					randomEyes3D.SetBlink (true);
				}
			}
			if (randomEyes3D.randomBlink) 
			{
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Random Blink On");
			} 
			else 
			{
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Random Blink Off");
			}
			#endregion
			
			#region When random blink is off, demonstrate programmatic blinking
			if (!randomEyes3D.randomBlink) 
			{
				yPos += (yGap + yHeight);
				if (GUI.Button (new Rect (xPos, yPos, xWidth, yHeight), "Blink")) 
				{
					randomEyes3D.Blink (0.075f);
				}
			}
			#endregion
			
			#region Toggle affinity to the target
			yPos += (yGap + yHeight);
			if (GUI.Button(new Rect(xPos, yPos, xWidth, yHeight), "Toggle Affinity"))
			{
				if (affinity)
				{
					affinity = false;
				}
				else
				{
					affinity = true;
				}
				affinitySet = true;
			}
			if (affinity) 
			{
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Affinity On: " + randomEyes3D.targetAffinityPercentage + "%");
			} 
			else 
			{
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Affinity Off");
			}
			if (affinitySet)
			{
				if (affinity)
				{
					randomEyes3D.SetTargetAffinity(true);
					randomEyes3D.SetLookTarget(target.gameObject);
				}
				else
				{
					randomEyes3D.SetTargetAffinity(false);
				}
				affinitySet = false;
			}
			#endregion
			
			#region Turn [Look Target] tracking on or off
			yPos += (yGap + yHeight);	
			if (GUI.Button (new Rect (xPos, yPos, xWidth, yHeight), "Toggle Tracking")) 
			{
				if (track) 
				{
					track = false;
				} 
				else 
				{
					track = true;
				}
				trackSet = true;
			}
			if (track) 
			{
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Tracking On");
			} 
			else 
			{
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Tracking Off");
			}
			#endregion
			
			#region Turn random eye movement on or off
			yPos += (yGap + yHeight);	
			if (GUI.Button (new Rect (xPos, yPos, xWidth, yHeight), "Toggle RandomEyes")) 
			{
				if (random) 
				{
					randomEyes3D.SetRandomEyes (false);
					random = false;
				} 
				else 
				{
					randomEyes3D.SetRandomEyes (true);
					random = true;
				}
			}
			#endregion
			#region Display the on/off status of random eye movement
			if (random)
			{
				GUI.Label(new Rect(xPos - 120, yPos, xWidth, yHeight), "Random Eyes On");
			}
			else
			{
				GUI.Label(new Rect(xPos - 120, yPos, xWidth, yHeight), "Random Eyes Off");
			}
			#endregion
			
			#region Display the on/off status, set target position to cursor position, and set the randomEyes3D.lookTarget
			if (track) 
			{
				targetPos = Input.mousePosition;
				targetPos.z = -mainCam.transform.position.z - -target.transform.position.z;
				target.transform.position = new Vector3 (
					mainCam.ScreenToWorldPoint (targetPos).x,
					mainCam.ScreenToWorldPoint (targetPos).y, -2f);
			}
			else 
			{
				target.transform.position = targetPosHome;
			}
			if (trackSet)
			{
				if (track) 
				{
					randomEyes3D.SetLookTarget(target.gameObject);
				} 
				else 
				{
					randomEyes3D.SetLookTarget(null);
				}
				trackSet = false;
			}
			#endregion

			if (!affinity && !track) randomEyes3D.SetLookTarget(null);
		}
	}
}