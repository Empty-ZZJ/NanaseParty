using UnityEngine;
using System.Collections;

/*
	Script usage instructions

	A general purpose waypoint script to allow waypoint movement using one of four [Animation Type]s:
		Once: Incrementally move toward the last waypoint, then stop. 
		Repeat: Incrementally move toward the last waypoint, then start again from the first waypoint.
		PingPong_Once: Incrementally move toward the last waypoint, then decrement back towards the first waypoint, then stop.
		PingPong_Repeat: Incrementally move toward the last waypoint, then decrement back towards the first waypoint, then repeat.

	1. Attach this script to an empty GameObject, or whatever game object you want to manage
		[CM_Waypoint] movements.
	2. Create [Empty] GameObjects in your scene, these are used as waypoints. It makes things
		easier to manage if you name them waypoint0, waypoint1, etc., and parent them to the 
		[CM_Waypoint] GameObject.
	3. Set your [Taget].
	4. Set your animation type.
	5. Set your [Speed] (movement speed from one waypoint to the next.
	6. Set the [Waypoints] [Size] to the number of waypoints you created.
	7. Inside each waypoint [Element], you will find the following:
		Waypoint: The [GameObject] your using as a waypoint.
		Direction: This is only used on sprites to determine the facing direction
		Delay: The amount of time to wait at each waypoint
*/

namespace CrazyMinnow.SALSA.Examples
{
	/// <summary>
	/// A waypoint properties class used in CM_Waypoints to track waypoints, target facing direction, and a delay time at each waypoint.
	/// </summary>
	[System.Serializable]
	public class CM_WaypointItems
	{
		public GameObject waypoint;
		public enum Direction { Left, Right }
		public Direction direction;
		public int delay;
	}

	/// <summary>
	/// A simple waypoint system that allows an object to move through an array of waypoints (Empty GameObjects).
	/// </summary>
	public class CM_Waypoints : MonoBehaviour
	{
		public GameObject target; // The object you want to traverse the waypoints
		public enum AnimationType { Once, Repeat, PingPong_Once, PingPong_Repeat } // Enum of the supported waypoint animation types
		public AnimationType animationType = AnimationType.PingPong_Repeat;
		public int StartingWaypoint = 0; // The index in the waypoints array you wish the target to start at
		public float speed = 3f; // Movement speed
		public CM_WaypointItems[] waypoints; // The array of waypoint items. Specifies waypoint, sprite direction, and delay at each waypoint

		private int curDestIndex = 0; // Current destination index (in the waypoints array)
		private Vector3 curDest; // Current destination position (waypoint position
		private bool countUp = true; // Used with the PingPong animation to track waypoint traversal up or down the waypoints array
		private float timer = 0f; // Used to time the waypoint delays
		private bool getTime = true; // Used to update the timer baseline
		
		/// <summary>
		/// Set the current destination to destination2
		/// </summary>
		void Start () 
		{
			if (waypoints != null)
			{
				curDestIndex = StartingWaypoint;
				curDest = waypoints[curDestIndex].waypoint.transform.position;
				target.transform.position = waypoints[curDestIndex].waypoint.transform.position;
			}
			else
			{
				Debug.LogError("Add waypoints to the waypoints array");
			}

			timer = Time.time;
		}
		
		/// <summary>
		/// Move the target GameObject towards its current destination waypoint and set it's facing direction
		/// </summary>
		void Update () 
		{
			target.transform.position = Vector3.MoveTowards(target.transform.position, curDest, Time.deltaTime * speed);
			
			if (waypoints[curDestIndex].direction == CM_WaypointItems.Direction.Left)
			{
				target.transform.eulerAngles = new Vector3(target.transform.eulerAngles.x, 0, target.transform.eulerAngles.z);
			}
			
			if (waypoints[curDestIndex].direction == CM_WaypointItems.Direction.Right)
			{
				target.transform.eulerAngles = new Vector3(target.transform.eulerAngles.x, 180, target.transform.eulerAngles.z);
			}

			WaypointCheck();
		}

		/// <summary>
		/// Waypoints the check.
		/// </summary>
		private void WaypointCheck()
		{
			switch (animationType)
			{
			case AnimationType.Once: // Move through the array of waypoint, then jump back to the first and do it again
				if (target.transform.position == curDest)
				{
					if (getTime)
					{
						timer = Time.time;
						getTime = false;
					}
					
					if (Time.time > (timer + waypoints[curDestIndex].delay))
					{
						if (countUp)
						{
							if (curDestIndex < waypoints.Length - 1)
							{
								curDestIndex++; // Assend the waypoint array
							}
						} 
						
						curDest = waypoints[curDestIndex].waypoint.transform.position;
						
						getTime = true;
					}
				}
				break;
			case AnimationType.Repeat: // Move through the array of waypoint, then jump back to the first and do it again
				if (target.transform.position == curDest)
				{
					if (getTime)
					{
						timer = Time.time;
						getTime = false;
					}
					
					if (Time.time > (timer + waypoints[curDestIndex].delay))
					{
						if (countUp)
						{
							if (curDestIndex < waypoints.Length - 1)
							{
								curDestIndex++; // Assend the waypoint array
							}
							else
							{	
								curDestIndex = 0; // Return to the first waypoint
								target.transform.position = waypoints[curDestIndex].waypoint.transform.position;
							}
						} 
						
						curDest = waypoints[curDestIndex].waypoint.transform.position;
						
						getTime = true;
					}
				}
				break;
			case AnimationType.PingPong_Once: // Move through the array of waypoints (0 to array end), then reverse (array end to 0)
				if (target.transform.position == curDest)
				{
					if (getTime)
					{
						timer = Time.time;
						getTime = false;
					}
					
					if (Time.time > (timer + waypoints[curDestIndex].delay))
					{
						if (countUp)
						{
							if (curDestIndex < waypoints.Length - 1)
							{
								curDestIndex++; // Assend the waypoint array
							}
							else
							{
								countUp = false;
							}
						} 
						
						if (!countUp)
						{
							if (curDestIndex > 0)
							{
								curDestIndex--; // Descend the waypoint array
							}
						}
						
						curDest = waypoints[curDestIndex].waypoint.transform.position;
						
						getTime = true;
					}
				}
				break;
			case AnimationType.PingPong_Repeat: // Move through the array of waypoints (0 to array end), then reverse (array end to 0), then repeat
				if (target.transform.position == curDest)
				{
					if (getTime)
					{
						timer = Time.time;
						getTime = false;
					}

					if (Time.time > (timer + waypoints[curDestIndex].delay))
					{
						if (countUp)
						{
							if (curDestIndex < waypoints.Length - 1)
							{
								curDestIndex++; // Assend the waypoint array
							}
							else
							{
								countUp = false;
							}
						} 

						if (!countUp)
						{
							if (curDestIndex > 0)
							{
								curDestIndex--; // Descend the waypoint array
							}
							else
							{
								countUp = true;
								curDestIndex++;
							}
						}

						curDest = waypoints[curDestIndex].waypoint.transform.position;

						getTime = true;
					}
				}
				break;
			}
		}

		/// <summary>
		/// Public function to set the animation type
		/// </summary>
		/// <param name="animationType">Animation type.</param>
		/// <param name="startingWaypoint">Starting waypoint.</param>
		public void SetAnimationType(AnimationType animType)
		{
			this.animationType = animType;
		}
	}
}