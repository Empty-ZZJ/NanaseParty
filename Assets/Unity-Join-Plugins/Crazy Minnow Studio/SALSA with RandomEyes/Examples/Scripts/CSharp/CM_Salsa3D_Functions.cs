using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

namespace CrazyMinnow.SALSA.Examples
{
	/// <summary>
	/// Demonstrates use of the Salsa3D public methods
	/// </summary>
	public class CM_Salsa3D_Functions : MonoBehaviour
	{
		public Salsa3D salsa3D; // Reference to the Salsa3D class
		public AudioClip[] audioClips; // An array of example sound to play
		
		private int clipIndex = 0; // Track audioClips index
		
		// These private variables are used to position buttons in the OnGUI method
		private int yPos = 0; // The Y position of a GUI button
		private int yGap = 10; // The vertical spacing between GUI buttons
		private int xWidth = 150; // Button and label width
		private int yHeight = 35; // Button and label height
		
		/// <summary>
		/// On start, try to get a local reference to Salsa3D
		/// </summary>
		void Start () 
		{
			if (!salsa3D) // salsa3D is null
			{
				salsa3D = (Salsa3D)FindObjectOfType(typeof(Salsa3D)); // Try to get a local reference to Salsa3D
			}
			
			if (audioClips.Length > 0)
			{
				salsa3D.SetAudioClip(audioClips[clipIndex]);
			}
		}

		/// <summary>
		/// Draw the GUI buttons
		/// </summary>
		void OnGUI()
		{
			yPos = 0; // Reset the button Y position
			
			#region Salsa3D Play, Pause, and Stop controls
			yPos += yGap;
			if (GUI.Button(new Rect(20, yPos, xWidth, yHeight), "Play"))
			{
				salsa3D.Play(); // Salsa3D Play method
			}
			
			yPos += (yGap + yHeight);
			if (GUI.Button(new Rect(20, yPos, xWidth, yHeight), "Pause"))
			{
				salsa3D.Pause(); // Salsa3D Pause method
			}
			
			yPos += (yGap + yHeight);
			if (GUI.Button(new Rect(20, yPos, xWidth, yHeight), "Stop"))
			{
				salsa3D.Stop(); // Salsa3D Stop method
			}
			#endregion
			
			#region Toggle which audio clip is set on Salsa3D
			yPos += (yGap + yHeight);
			if (GUI.Button(new Rect(20, yPos, xWidth, yHeight), "Set audio clip"))
			{
				if (clipIndex < audioClips.Length - 1)
				{
					clipIndex++;
					salsa3D.SetAudioClip(audioClips[clipIndex]);
				}
				else
				{
					clipIndex = 0;
					salsa3D.SetAudioClip(audioClips[clipIndex]);
				}
			}
			#endregion
			#region Display the currently selected audio clip
			GUI.Label(new Rect(30 + xWidth, yPos, xWidth, yHeight), "Clip " + audioClips[clipIndex].name);
			#endregion
		}
	}
}