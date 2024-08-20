using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace
using CrazyMinnow.SALSA.Examples;

/*
 Script usage instructions 

 CM_DialogSystem is a basic dialog system that demonstrates one approach to implementing Salsa into your game project. 
 
 1. Attach this script to an empty GameObject called [Game Manager], or whatever game object you want to manage your dialog.
 2. Set the [Npc Dialog] [Size] property to the number of NPC lines you wish to have on one or more NPC's.
 3. Link the following:
 	Npc: NPC GameObject that has the Salsa component attached to it.
 	Npc Text: The text script of the NPC's line.
 	Npc Audio: The audio clip that Salsa will process for this NPC.
 	Player Response: If you want the player to have response options to this line, set the 
 		[Player Response] [Size] property to the number of Player line options you wish to have.
 		Each player response consists of the following:
 			Player: Player GameObject that has the Salsa component attached to it.
 			Player Text: The text script of the Players's line.
 			Player Audio: The audio clip that Salsa will process for the Player.
 			NPC Dialog Index: The next [NPC Dialog] [Element] you wish to player 
 				after this player response is selected. (Allows basic conversation branching)
 			End Dialog: When checked, selecting this [Player Response] will end the 
 				dialog after this [Player Audio] file finishes playing.
		End Dialog: When checked, the dialog will end after this [Npc Dialog] [Element]'s
 			[Npc Audio] file finishes playing.
 4. Be sure to Set the GameObject, with this script attached to it, as a [BroadCast Receiver] of all Salsa enabled game actors
 	so this script will recieve [Salsa_OnTalkStatusChanged] events from Salsa's talk status changed events.
*/

namespace CrazyMinnow.SALSA.Examples
{
	/// <summary>
	/// A properties class that defines a Salsa GameObject and a Salsa type.
	/// It's used in the GetSalsaType function of the CM_DialogSystem class
	/// to store the GameObject and Salsa type (Salsa2D or Salsa3D) that was
	/// detected in the GetSalsaType function. This class assits in allowing the
	/// CM_DialogSystem class to work on Salsa2D and Salsa3D powered characters.
	/// </summary>
	public class CM_SalsaTypeAndObject
	{
		public GameObject salsaGameObject;
		public enum SalsaTypeOf { Salsa2D, Salsa3D }
		public SalsaTypeOf salsaType = SalsaTypeOf.Salsa2D;
	}

	/// <summary>
	/// A properties class that defines player dialog response details.
	/// It's used in the GetSalsaType function of the CM_NPCDialog class.
	/// </summary>
	[System.Serializable]
	public class CM_PlayerResponse
	{
		public GameObject player; // Player GameObject that has the Salsa component attached
		public string playerText; // Player dialog text to display in the GUI
		public AudioClip playerAudio; // Player audio dialog to play
		public int npcDialogIndex; // The NPC dialog index triggered by this player response
		public bool endDialog = false; // Will this response end all dialog
	}

	/// <summary>
	/// A properties class that defines NPC dialog, and stores player dialog responses.
	/// It's used in the GetSalsaType function of the CM_DialogSystem class.
	/// </summary>
	[System.Serializable]
	public class CM_NPCDialog
	{
		public GameObject npc; // NPC GameObject that has the Salsa component attached
		public string npcText; // NPC dialog text to display in the GUI
		public AudioClip npcAudio; // NPC audio dialog to play
		public CM_PlayerResponse[] playerResponse; // Array of player dialog responses
		public bool endDialog = false; // Will this response end all dialog
	}

	/// <summary>
	/// A basic dialog system that demonstrates one approach to implementing Salsa into your game project. 
	/// </summary>
	public class CM_DialogSystem : MonoBehaviour 
	{
		public CM_NPCDialog[] npcDialog; // Array of NPC dialog

		private Salsa2D salsa2D; // If the detected character using Salsa2D, this variable is used
		private Salsa3D salsa3D; // If the detected character using Salsa3D, this variable is used
		private int npcDialogIndexTracker = 0; // Tracks the current NPC dialog index
		private bool showNPCDialog = true; // Tracks the visible status of the NPC dialog text
		private bool showPlayerResponses = false; // Tracks the visible status of the player dialog text
		private bool endDialogPlayer = false; // Tracks when the player ends the dialog
		private bool endDialogNpc = false; // Tracks whe the NPC ends the dialog
		private CM_SalsaTypeAndObject salsaTypObj; // See comments for the CM_SalsaTypeAndObject class listed above

		/// <summary>
		/// Determines if the NPC is using Salsa2D or Salsa3D, gets reference to 
		/// the component, sets the first NPC audio clip, and plays the audio clip
		/// </summary>
		void Start()
		{
			this.salsaTypObj = GetSalsaType(npcDialog[npcDialogIndexTracker].npc);

			if (this.salsaTypObj.salsaType == CM_SalsaTypeAndObject.SalsaTypeOf.Salsa3D)
			{
				salsa3D = this.salsaTypObj.salsaGameObject.GetComponent<Salsa3D>();
				salsa3D.SetAudioClip(npcDialog[npcDialogIndexTracker].npcAudio);
				salsa3D.Play();
			}

			if (this.salsaTypObj.salsaType == CM_SalsaTypeAndObject.SalsaTypeOf.Salsa2D)
			{
				salsa2D = this.salsaTypObj.salsaGameObject.GetComponent<Salsa2D>();
				salsa2D.SetAudioClip(npcDialog[npcDialogIndexTracker].npcAudio);
				salsa2D.Play();
			}
		}		

		/// <summary>
		/// Method is called by SALSA broadcast when the talk status has changed
		/// </summary>
		/// <param name="status">Status.</param>
		void Salsa_OnTalkStatusChanged(SalsaStatus status)
		{
			// Npc has stopped talking
			if (!status.isTalking && status.talkerName == npcDialog[npcDialogIndexTracker].npc.name)
			{
				// NPC says end dialog
				if (npcDialog[npcDialogIndexTracker].endDialog)
				{
					EndDialog();
				}

				if (!endDialogNpc)
				{
					// There are no player responses to this NPC dialog
					if (npcDialog[npcDialogIndexTracker].playerResponse.Length == 0)
					{
						// We're not at the end of the [Npc Dialog] array
						if (npcDialogIndexTracker < npcDialog.Length - 1)
						{
							npcDialogIndexTracker++; // Increment to the Npc dialog
							showNPCDialog = true; // Show NCP dialog
							Start(); // Get Salsa type, set audio clip, and play
						}
					}
					else // There are player responses to this NPC dialog
					{
						showPlayerResponses = true;
					}
				}
			}

			// Player has stopped talking
			if (!status.isTalking && status.talkerName != npcDialog[npcDialogIndexTracker].npc.name)
			{
				if (!endDialogNpc || !endDialogPlayer)
				{
					showNPCDialog = true; // Show NCP dialog
					Start(); // Get Salsa type, set audio clip, and play
				}
			}
		}

		/// <summary>
		/// NPC dialog text and player dialog response text GUI
		/// </summary>
		void OnGUI()
		{
			int yPos = 0;
			int yStart = 20;
			int yIncrement = 40;

			// No end dialog flags are set
			if (!endDialogNpc || !endDialogPlayer)
			{
				if (showNPCDialog && !endDialogPlayer)
				{
					GUI.Label(new Rect(20, yStart, 300, 35), npcDialog[npcDialogIndexTracker].npcText);
				}
			}

			if (showPlayerResponses)
			{
				yPos = yStart;
				// Loop through all player responses to the current NPC dialog
				for (int i = 0; i < npcDialog[npcDialogIndexTracker].playerResponse.Length; i++)
				{
					// Show response dialog text buttons
					if (GUI.Button(new Rect(Screen.width - 320, yPos, 300, 35), npcDialog[npcDialogIndexTracker].playerResponse[i].playerText))
					{
						// If this button was selected, get the Salsa type and GameObject
						this.salsaTypObj = GetSalsaType(npcDialog[npcDialogIndexTracker].playerResponse[i].player);

						// If Salsa3D
						if (this.salsaTypObj.salsaType == CM_SalsaTypeAndObject.SalsaTypeOf.Salsa3D)
						{
							salsa3D = this.salsaTypObj.salsaGameObject.GetComponent<Salsa3D>();
							salsa3D.SetAudioClip(npcDialog[npcDialogIndexTracker].playerResponse[i].playerAudio);
							salsa3D.Play();
						}

						// If Salsa2D
						if (this.salsaTypObj.salsaType == CM_SalsaTypeAndObject.SalsaTypeOf.Salsa2D)
						{
							salsa2D = this.salsaTypObj.salsaGameObject.GetComponent<Salsa2D>();
							salsa2D.SetAudioClip(npcDialog[npcDialogIndexTracker].playerResponse[i].playerAudio);
							salsa2D.Play();
						}

						// Check/Set the player end dialog flag
						endDialogPlayer = npcDialog[npcDialogIndexTracker].playerResponse[i].endDialog;
						// Set the next NPC dialog index
						npcDialogIndexTracker = npcDialog[npcDialogIndexTracker].playerResponse[i].npcDialogIndex;
						showNPCDialog = false; // Hide the NPC dialog
						showPlayerResponses = false; // Hide the player responses
					}
					yPos += yIncrement;
				}
			}
		}

		/// <summary>
		/// Gets the game object of the character with Salsa3D or Salsa2D attached, 
		/// and returns an instance of the CM_SalsaTypeAndObject properties class 
		/// with the Salsa GameObject and SalsaType
		/// </summary>
		/// <returns>The salsa.</returns>
		/// <param name="character">Character.</param>
		private CM_SalsaTypeAndObject GetSalsaType(GameObject character)
		{
			CM_SalsaTypeAndObject salsaTypObj = new CM_SalsaTypeAndObject();
			
			if (character.GetComponent<Salsa2D>() != null) 
			{ 
				salsaTypObj.salsaGameObject = character.GetComponent<Salsa2D>().gameObject;
				salsaTypObj.salsaType = CM_SalsaTypeAndObject.SalsaTypeOf.Salsa2D;
			}
			else if (character.GetComponent<Salsa3D>() != null) 
			{ 
				salsaTypObj.salsaGameObject = character.GetComponent<Salsa3D>().gameObject;
				salsaTypObj.salsaType = CM_SalsaTypeAndObject.SalsaTypeOf.Salsa3D;
			}
			
			return salsaTypObj;
		}

		/// <summary>
		/// Ends the dialog by setting the NPC and Player end dialog flags to true,
		/// and setting their respective show dialog flags to false.
		/// </summary>
		private void EndDialog()
		{
			endDialogNpc = true;
			endDialogPlayer = true;
			showNPCDialog = false;
			showPlayerResponses = false;
		}

		/// <summary>
		/// Reset the dialog system at runtime
		/// </summary>
		public void ResetDialog()
		{
			npcDialogIndexTracker = 0;
			endDialogNpc = false;
			endDialogPlayer = false;
			showNPCDialog = true;
			showPlayerResponses = false;
			
			this.salsaTypObj = GetSalsaType(npcDialog[npcDialogIndexTracker].npc);
			
			if (this.salsaTypObj.salsaType == CM_SalsaTypeAndObject.SalsaTypeOf.Salsa3D)
			{
				salsa3D = this.salsaTypObj.salsaGameObject.GetComponent<Salsa3D>();
				salsa3D.Stop();
			}
			
			if (this.salsaTypObj.salsaType == CM_SalsaTypeAndObject.SalsaTypeOf.Salsa2D)
			{
				salsa2D = this.salsaTypObj.salsaGameObject.GetComponent<Salsa2D>();
				salsa2D.Stop();
			}
			
			if (npcDialog[npcDialogIndexTracker].playerResponse.Length > 0)
			{
				this.salsaTypObj = GetSalsaType(npcDialog[npcDialogIndexTracker].playerResponse[0].player);
				
				if (this.salsaTypObj.salsaType == CM_SalsaTypeAndObject.SalsaTypeOf.Salsa3D)
				{
					salsa3D = this.salsaTypObj.salsaGameObject.GetComponent<Salsa3D>();
					salsa3D.Stop();
				}
				
				if (this.salsaTypObj.salsaType == CM_SalsaTypeAndObject.SalsaTypeOf.Salsa2D)
				{
					salsa2D = this.salsaTypObj.salsaGameObject.GetComponent<Salsa2D>();
					salsa2D.Stop();
				}
			}
			
			Start();
			showNPCDialog = true;
		}
	}
}