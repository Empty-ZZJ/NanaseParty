using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

/*
	Script usage instructions

	CM_Ethan_Demo is a script that uses the SALSA and RandomEyes event systems to drive
	emotional expressions linked to RandomEyes as [Custom Shapes]. It uses
	Salsa_OnTalkStatusChanged events to choreograph the scene with expression changes,
	dialog changes, and lookTargets. It also uses the RandomEyes_OnLookStatusChanged 
	to trigger additional emotional expressions.
*/

namespace CrazyMinnow.SALSA.Examples
{
	public class CM_Ethan_Demo : MonoBehaviour 
	{
		public Salsa3D salsa;
		public RandomEyes3D randomEyes;
		public GameObject[] lookTargets;
		public AudioClip[] clips;
		public bool salsaEvents = false;
		public bool randomEyesLookEvents = false;
		public bool randomEyesShapeEvents = false;

		/// <summary>
		/// Play SALSA with the first audio clip after a 1 second delay
		/// </summary>
		void Start()
		{
			StartCoroutine(WaitStart(1f));
		}

		/// <summary>
		/// Here we use the Salsa on talk status changed event to: 
		/// Listen for audio clip starts and stops
		/// Call custom shape coroutines
		/// Set and play the next dialog clip
		/// Look at specific GameObjects
		/// </summary>
		/// <param name="status">Status.</param>
		void Salsa_OnTalkStatusChanged(SalsaStatus status)
		{
			if (salsaEvents)
			{
				Debug.Log("Salsa_OnTalkStatusChanged:" +
				          " instance(" + status.instance.GetType() + ")," +
				          " talkerName(" + status.talkerName + ")," +
				          ((status.isTalking) ? "started" : "finished") + " saying " + status.clipName);
			}

			if (status.clipName == clips[0].name && status.isTalking) // Line 0 start
			{
				StartCoroutine(Look(0f, 2f, lookTargets[0])); // Look at camera
				StartCoroutine(Look(5f, 2f, lookTargets[1])); // Look at door
			}
			if (status.clipName == clips[0].name && !status.isTalking) // Line 0 stop
			{
				salsa.SetAudioClip(clips[1]);
				salsa.Play();
			}


			if (status.clipName == clips[1].name && status.isTalking) // Line 1 start
			{
				StartCoroutine(Look(0f, 3f, lookTargets[2])); // // Look at vent
			}
			if (status.clipName == clips[1].name && !status.isTalking) // Line 1 stop
			{
				salsa.SetAudioClip(clips[2]);
				salsa.Play();
			}


			if (status.clipName == clips[2].name && status.isTalking) // Line 2 start
			{
				StartCoroutine(Look(6f, 5f, lookTargets[0])); // // Look at camera
			}
			if (status.clipName == clips[2].name && !status.isTalking) // Line 2 stop
			{
				StartCoroutine(Look(0f, 2.5f, lookTargets[0]));  // Look at camera for 2.5 sec
				randomEyes.SetCustomShapeRandom(false); // Disable random custom shapes
				randomEyes.SetCustomShapeOverride("brows_inner_up", 2f); // Override brows_inner_up
				randomEyes.SetCustomShapeOverride("smile", 2f); // Overrid smile
			}
		}

		/// <summary>
		/// RandomEyes on look status changed lets us know when 
		/// the eye postion has finished moving to the next position.
		/// In this simple example scene, we are using the random
		/// look positions to trigger custom shape emotions.
		/// </summary>
		/// <param name="status">Status.</param>
		void RandomEyes_OnLookStatusChanged(RandomEyesLookStatus status)
		{
			if (randomEyesLookEvents)
			{
				Debug.Log("RandomEyes_OnLookStatusChanged:" +
				          " instance(" + status.instance.GetType() + ")," +
				          " name(" + status.instance.name + ")," +
				          " blendSpeed(" + status.blendSpeed + ")," +
				          " rangeOfMotion(" + status.rangeOfMotion + ")");
			}
		}

		/// <summary>
		/// We can also respond to custom shape changes
		/// </summary>
		/// <param name="status">Status.</param>
		void RandomEyes_OnCustomShapeChanged(RandomEyesCustomShapeStatus status)
		{
			if (randomEyesShapeEvents)
			{
				Debug.Log("RandomEyes_OnCustomShapeChanged:" +
				          " instance(" + status.instance.GetType() + ")," +
				          " name(" + status.instance.name + ")," +
				          " shapeIndex(" + status.shapeIndex + ")," +
				          " shapeName(" + status.shapeName + ")," +
				          " overrideOn(" + status.overrideOn + ")," +
				          " isOn(" + status.isOn + ")," +
				          " blendSpeed(" + status.blendSpeed + ")," +
				          " rangeOfMotion(" + status.rangeOfMotion + ")");
			}
		}

		/// <summary>
		/// A coroutine to track a GameObject with a pre-delay and a track duration
		/// </summary>
		/// <param name="preDelay">Pre delay.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="customShapeIndex">Custom shape index.</param>
		IEnumerator Look(float preDelay, float duration, GameObject lookTarget)
		{
			yield return new WaitForSeconds(preDelay);

			randomEyes.SetLookTarget(lookTarget);

			yield return new WaitForSeconds(duration);

			randomEyes.SetLookTarget(null);
		}

		/// <summary>
		/// A coroutine to add a delay before playing the first clip.
		/// This is a hack to sync up the dialog to the mocap data.
		/// </summary>
		/// <returns>The start.</returns>
		/// <param name="duration">Duration.</param>
		IEnumerator WaitStart(float duration)
		{
			yield return new WaitForSeconds(duration);

			salsa.SetAudioClip(clips[0]);
			salsa.Play();
		}
	}
}