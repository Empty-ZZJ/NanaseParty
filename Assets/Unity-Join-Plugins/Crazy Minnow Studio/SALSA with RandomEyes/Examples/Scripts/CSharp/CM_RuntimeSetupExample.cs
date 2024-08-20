using UnityEngine;
using System.Collections;
using CrazyMinnow.SALSA;

namespace CrazyMinnow.SALSA
{
	/// <summary>
	/// Script usage instructions 
	/// 
	/// This script demonstrate setting up Salsa3D and RandomEyes3D at runtime from script
	/// 
	/// 1. Place this script on the character object that contains the SkinnedMeshRenderer and your BlendShapes.
	/// 2. Run the scene.
	/// 3. Check/uncheck the loadSalsaAndRandomEyes property to add, configure, or remove Salsa3D and RandomEyes3D components.
	/// </summary>
	public class CM_RuntimeSetupExample : MonoBehaviour 
	{
		public AudioClip audioClip; // Link an AudioClip for Salsa3D to play
		public GameObject broadcastReciever; // Link a GameObject that contains scripts to catch SALSA and RandomEyes events
		public bool loadSalsaAndRandomEyes = false; // Click this in the inspector to add, configure, and remove a Salsa3D and RandomEyes3D component at runtime

		private Salsa3D salsa3D;
		private RandomEyes3D re3D;
		
		void Start () 
		{
			StartCoroutine(RemoveComponents());
		}

		void Update()
		{
			// Add the components when checked and not already loaded
			if (loadSalsaAndRandomEyes && !salsa3D && !re3D)
			{
				// Salsa3D
				gameObject.AddComponent<Salsa3D>(); // Add a Salsa3D component
				salsa3D = GetComponent<Salsa3D>(); // Get reference to the Salsa3D component
				salsa3D.skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>(); // Link the SkinnedMeshRenderer
				salsa3D.saySmallIndex = 0; // Set saySmall BlendShape index
				salsa3D.sayMediumIndex = 1; // Set sayMedium BlendShape index
				salsa3D.sayLargeIndex = 2; // Set sayLarge BlendShape index
				salsa3D.SetAudioClip(audioClip); // Set AudioClip
				// Or set the AudioClip from a clip in any Resources folder
				//salsa3D.SetAudioClip((Resources.Load("EthanEcho0", typeof(AudioClip)) as AudioClip));
				salsa3D.saySmallTrigger = 0.002f; // Set the saySmall amplitude trigger
				salsa3D.sayMediumTrigger = 0.004f; // Set the sayMedium amplitude trigger
				salsa3D.sayLargeTrigger = 0.006f; // Set the sayLarge amplitude trigger
				salsa3D.audioUpdateDelay = 0.08f; // Set the amplitutde sample update delay
				salsa3D.blendSpeed = 10f; // Set the blend speed
				salsa3D.rangeOfMotion = 100f; // Set the range of motion

				salsa3D.broadcast = true; // Enable talk event broadcasts
				salsa3D.broadcastReceivers = new GameObject[1]; // Creat a new array of broadcast receivers
				salsa3D.broadcastReceivers[0] = broadcastReciever; // Link to a GameObject setup to listen for SALSA talk events
				salsa3D.expandBroadcast = true; // Expand the broadcast recievers array in the inspector

				salsa3D.Play(); // Begin lip sync


				// RandomEyes3D
				gameObject.AddComponent<RandomEyes3D>(); // Add a RandomEyes3D component
				re3D = GetComponent<RandomEyes3D>(); // Get reference to the RandomEyes3D component
				re3D.skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>(); // Link the SkinnedMeshRenderer
				re3D.lookUpIndex = 3; // Set the lookUp BlendShape index
				re3D.lookDownIndex = 4; // Set the lookDown BlendShape index
				re3D.lookLeftIndex = 5; // Set the lookLeft BlendShape index
				re3D.lookRightIndex = 6; // Set the lookRight BlendShape index
				re3D.blinkIndex = 7; // Set the blink BlendShape index
				re3D.rangeOfMotion = 100f; // Set the eyes range of motion
				re3D.blendSpeed = 10f; // Set the eyes blend speed
				re3D.blinkDuration = 0.05f; // Set the blink duration
				re3D.blinkSpeed = 20f; // Set the blink speed
				re3D.SetOpenMax(0f); // Set the max eye open position, 0=max
				re3D.SetCloseMax(100f); // Set the max eye close position, 100=max
				re3D.SetRandomEyes(true); // Enable random eye movement
				re3D.SetBlink(true); // Enable random blink

				re3D.AutoLinkCustomShapes(true, salsa3D); // Automatically link all available BlendShapes while excluding eye and mouth shapes
				re3D.expandCustomShapes = true; // Expand the custom shapes array in the inspector

				re3D.broadcastCS = true; // Enable cust shape event broadcasts
				re3D.broadcastCSReceivers = new GameObject[1]; // Creat a new array of broadcast receivers
				re3D.broadcastCSReceivers[0] = GameObject.Find("Broadcasts"); // Link to a GameObject setup to listen for RandomEyes custom shape events
				re3D.expandBroadcastCS = true; // Expand the broadcast recievers array in the inspector
			}
		}

		/// <summary>
		/// Removes the components when unchecked
		/// </summary>
		/// <returns>The components.</returns>
		IEnumerator RemoveComponents()
		{
			while (true)
			{
				if (!loadSalsaAndRandomEyes && salsa3D && re3D)
				{
					DestroyImmediate(salsa3D); // Destroy Sals3D
					DestroyImmediate(GetComponent<AudioSource>()); // Destroy Salsa's AudioSource

					DestroyImmediate(re3D); // Destroy RandomEyes3D
				}

				yield return new WaitForSeconds(0.1f);
			}
		}
	}
}