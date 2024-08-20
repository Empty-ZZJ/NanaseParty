using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

/*
	Script usage instructions

	This class demonstrates the use of the RandomEyes 2D/3D [Broadcast Eye Events], 
	and the RandomEyes 3D [Broadcast Custom Shape Events], to catch broadcasted
	RandomEyes_OnLookStatusChanged and RandomEyes_OnCustomShapeChanged events.

	1. Create a GameObject and attach this script to it.
	2. Set this GameObject as a [Broadcast Receiver] in RandomEyes.
*/

namespace CrazyMinnow.SALSA.Examples
{
	public class CM_RandomEyesCustomShapeIterator : MonoBehaviour 
	{
		public RandomEyes3D randomEyes3D;

		private int customIndex = 0;

		void Start()
		{
			if (!randomEyes3D)
			{
				randomEyes3D = GetComponent<RandomEyes3D>();
			}
		}

		void RandomEyes_OnCustomShapeChanged(RandomEyesCustomShapeStatus customShape)
		{
			if (customShape.isOn == true)
			{
				if (customIndex < randomEyes3D.customShapes.Length-1)
				{
					customIndex++;
				}
				else
				{
					customIndex = 0;
				}
				
				randomEyes3D.SetCustomShape(randomEyes3D.customShapes[customIndex].shapeName);
			}
		}
	}
}