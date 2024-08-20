using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

/*
	Script usage instructions

	This class demonstrates the use of the Salsa [Broadcast Receivers] 
	property to catch broadcast events from SALSA.

	1. Create a GameObject and attach this script to it.
	2. Set this GameObject as a [Broadcast Receiver] in Salsa.
*/

namespace CrazyMinnow.SALSA.Examples
{
	public class CM_SalsaBroadcastEventTester : MonoBehaviour 
	{

		void Salsa_OnTalkStatusChanged(SalsaStatus status)
		{
			Debug.Log("Salsa_OnTalkStatusChanged:" +
			          " instance(" + status.instance.GetType() + ")," +
			          " talkerName(" + status.talkerName + ")," +
			          ((status.isTalking) ? "started" : "finished") + " saying " + status.clipName);
		}
	}
}