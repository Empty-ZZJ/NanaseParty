using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

namespace CrazyMinnow.SALSA.Examples
{
	/// <summary>
	/// This class provides a simple example of how you can use a collision trigger
	/// to set the RandomEyes2D or RandomEyes3D lookTarget to enable eye tracking.
	/// </summary>
	public class CM_RandomEyesTriggerTracking : MonoBehaviour 
	{
		public GameObject lookTarget;
		public bool emitDebug = true;

		private RandomEyes2D randomEyes2D;
		private RandomEyes3D randomEyes3D;
		private GameObject randomEyes;

		/// <summary>
		/// Get reference to a RandomEyes component
		/// </summary>
		void Start () 
		{
			randomEyes2D = GetComponent<RandomEyes2D>();
			if (randomEyes2D)
			{
				randomEyes = randomEyes2D.gameObject;
			}

			randomEyes3D = GetComponent<RandomEyes3D>();
			if (randomEyes3D)
			{
				randomEyes = randomEyes3D.gameObject;
			}
		}

		/// <summary>
		/// OnTriggerEnter, set the RandomEyes lookTarget to the collider GameObject
		/// </summary>
		/// <param name="col">Col.</param>
		void OnTriggerEnter(Collider col)
		{
			if (randomEyes2D) randomEyes2D.SetLookTarget(col.gameObject);
			if (randomEyes3D) randomEyes3D.SetLookTarget(col.gameObject);
			if (emitDebug) Debug.Log(randomEyes.name + " OnTriggerEnter2D triggered");
		}

		/// <summary>
		/// OnTriggerExit, clear the RandomEyes lookTarget
		/// </summary>
		/// <param name="col">Col.</param>
		void OnTriggerExit(Collider col)
		{
			if (randomEyes2D) randomEyes2D.SetLookTarget(null);
			if (randomEyes3D) randomEyes3D.SetLookTarget(null);
			if (emitDebug) Debug.Log(randomEyes.name + " OnTriggerExit2D triggered");
		}
	}
}