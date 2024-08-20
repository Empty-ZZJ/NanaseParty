using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

namespace CrazyMinnow.SALSA.Examples
{
	/// <summary>
	/// Demonstrates use of the Salsa2D public methods
	/// </summary>
	public class CM_Salsa2D_Functions : MonoBehaviour
	{
		public Salsa2D salsa2D; // Reference to the Salsa2D class
		public AudioClip[] audioClips; // An array of example sound to play

		private int clipIndex = 0; // Track audioClips index

		// These private variables are used to position buttons in the OnGUI method
		private int yPos = 0; // The Y position of a GUI button
		private int yGap = 5; // The vertical spacing between GUI buttons
		private int xWidth = 150; // Button and label width
		private int yHeight = 30; // Button and label height
		
		/// <summary>
		/// On start, try to get a local reference to Salsa2D
		/// </summary>
		void Start () 
		{
			if (!salsa2D) // salsa2D is null
			{
				salsa2D = (Salsa2D)FindObjectOfType(typeof(Salsa2D)); // Try to get a local reference to Salsa2D
			}

			if (audioClips.Length > 0)
			{
				salsa2D.SetAudioClip(audioClips[clipIndex]);
			}
		}

		/// <summary>
		/// Draw the GUI buttons
		/// </summary>
		void OnGUI()
		{
			yPos = 0; // Reset the button Y position

			#region Salsa2D Play, Pause, and Stop controls
			yPos += yGap;
			if (GUI.Button(new Rect(20, yPos, xWidth, yHeight), "Play"))
			{
				salsa2D.Play(); // Salsa3D Play method
			}
			
			yPos += (yGap + yHeight);
			if (GUI.Button(new Rect(20, yPos, xWidth, yHeight), "Pause"))
			{
				salsa2D.Pause(); // Salsa3D Pause method
			}

			yPos += (yGap + yHeight);
			if (GUI.Button(new Rect(20, yPos, xWidth, yHeight), "Stop"))
			{
				salsa2D.Stop(); // Salsa3D Stop method
			}
			#endregion

			#region Toggle which audio clip is set on Salsa2D
			yPos += (yGap + yHeight);
			if (GUI.Button(new Rect(20, yPos, xWidth, yHeight), "Set audio clip"))
			{
				if (clipIndex < audioClips.Length - 1)
				{
					clipIndex++;
					salsa2D.SetAudioClip(audioClips[clipIndex]);
				}
				else
				{
					clipIndex = 0;
					salsa2D.SetAudioClip(audioClips[clipIndex]);
				}
			}
			#endregion
			#region Display the currently selected audio clip
			GUI.Label(new Rect(30 + xWidth, yPos, xWidth, yHeight), "Clip " + audioClips[clipIndex].name);
			#endregion
		}
	}
}