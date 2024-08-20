using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

namespace CrazyMinnow.SALSA.Examples
{
	/// <summary>
	/// Demonstrates use of the RandomEyes2D public methods
	/// </summary>
	public class CM_RandomEyes2D_Functions : MonoBehaviour 
	{
		public RandomEyes2D randomEyes2D; // Reference to the RandomEyes2D class
		public Camera mainCam; // Game camera
		public SpriteRenderer target; // SpriteRenderer target used to demonstrate eye target tracking
		
		private bool random = true; // Toggle [Random Eyes] checkbox on the RandomEyes inspector
		private bool affinity = false; // Toggle [Target Affinity] for the target
		private bool affinitySet = true; // Used to make sure SetTargetAffinity only fires once
		private bool track = false; // Set/clear the [Look Target] on the RandomEyes inspector
		private bool trackSet = true; // Used to make sure SetLookTarget only fires once
		private Vector3 targetPosHome; // Home position
		private Vector3 targetPos; // Target sprite mapped to cursor position to demonstrate [Look Target] eye tracking
		
		// These private variables are used to position buttons in the OnGUI method
		private int xPos = 0; // The Z position of a GUI button
		private int yPos = 0; // The Y position of a GUI button
		private int yGap = 5; // The vertical spacing between GUI buttons
		private int xWidth = 150; // The X width of GUI buttons
		private int yHeight = 30; // The Y height of GUI buttons
		
		/// <summary>
		/// On start, try to get a local reference to the RandomEyes2D class and the scene  camera
		/// </summary>
		void Start()
		{
			if (!randomEyes2D) // randomEyes2D is null
				randomEyes2D = (RandomEyes2D)FindObjectOfType(typeof(RandomEyes2D)); // Try to get a local reference to RandomEyes2D
			
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
				if (randomEyes2D.randomBlink) 
				{
					randomEyes2D.SetBlink (false);
				} 
				else 
				{
					randomEyes2D.SetBlink (true);
				}
			}
			if (randomEyes2D.randomBlink) 
			{
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Random Blink On");
			} 
			else 
			{
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Random Blink Off");
			}
			#endregion
			
			#region When random blink is off, demonstrate programmatic blinking
			if (!randomEyes2D.randomBlink) 
			{
				yPos += (yGap + yHeight);
				if (GUI.Button (new Rect (xPos, yPos, xWidth, yHeight), "Blink")) 
				{
					randomEyes2D.Blink (0.075f);
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
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Affinity On: " + randomEyes2D.targetAffinityPercentage + "%");
			} 
			else 
			{
				GUI.Label (new Rect (xPos - 120, yPos, xWidth, yHeight), "Affinity Off");
			}
			if (affinitySet)
			{
				if (affinity)
				{
					randomEyes2D.SetTargetAffinity(true);
					randomEyes2D.SetLookTarget(target.gameObject);
				}
				else
				{
					randomEyes2D.SetTargetAffinity(false);
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
					randomEyes2D.SetRandomEyes (false);
					random = false;
				} 
				else 
				{
					randomEyes2D.SetRandomEyes (true);
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
			
			#region Display the on/off status, set target position to cursor position, and set the randomEyes2D.lookTarget
			if (track) 
			{
				targetPos = Input.mousePosition;
				targetPos.z = -mainCam.transform.position.z - -target.transform.position.z;
				target.transform.position = new Vector3 (
					mainCam.ScreenToWorldPoint (targetPos).x,
					mainCam.ScreenToWorldPoint (targetPos).y, -0.5f);
			} 
			else 
			{
				target.transform.position = targetPosHome;
			}
			if (trackSet)
			{
				if (track)
				{
					randomEyes2D.SetLookTarget(target.gameObject);
				}
				else
				{
					randomEyes2D.SetLookTarget(null);
				}
				trackSet = false;
			}
			#endregion

			if (!affinity && !track) randomEyes2D.SetLookTarget(null);
		}
	}
}