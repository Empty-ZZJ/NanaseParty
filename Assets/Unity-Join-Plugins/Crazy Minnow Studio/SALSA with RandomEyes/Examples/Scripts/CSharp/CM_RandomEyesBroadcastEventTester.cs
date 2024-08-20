using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

/*
	Script usage instructions

	This class demonstrates the use of the RandomEyes 2D/3D [Broadcast Eye Events], 
	and the RandomEyes 3D [Broadcast Custom Shape Events], to catch
	RandomEyes_OnLookStatusChanged and RandomEyes_OnCustomShapeChanged events.

	1. Create a GameObject and attach this script to it.
	2. Set this GameObject as a [Broadcast Receiver] in RandomEyes.
*/

namespace CrazyMinnow.SALSA.Examples
{
	public class CM_RandomEyesBroadcastEventTester : MonoBehaviour 
	{
		void RandomEyes_OnLookStatusChanged(RandomEyesLookStatus status)
		{
			Debug.Log("RandomEyes_OnLookStatusChanged:" +
			          " instance(" + status.instance.GetType() + ")," +
			          " name(" + status.instance.name + ")," +
			          " blendSpeed(" + status.blendSpeed + ")," +
			          " rangeOfMotion(" + status.rangeOfMotion + ")");
		}

		void RandomEyes_OnCustomShapeChanged(RandomEyesCustomShapeStatus status)
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
}