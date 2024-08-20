using UnityEngine;
using System.Collections;

using CrazyMinnow.SALSA; // Import SALSA from the CrazyMinnow namespace

/*
	Script usage instructions

	A simple waypoints script to allow waypoint movement triggered by Salsa_OnTalkStatusChanged events.
	The script lets you use the Start and End of [AudioClip]'s used by Salsa to trigger waypoint
	destination and movement speed changes. When set as a [Broadcast Receiver] in Salsa, it listens
	for the start and end of audio clips from Salsa's [Salsa_OnTalkStatusChanged] events. When a Salsa
	[Audio Clip] matches and CM_SalsaWaypoints [Audio Clip], the waypoint details are updated.

	1. Attach this script to an empty GameObject, or whatever game object you want to manage
		[Salsa_OnTalkStatusChanged] event-based waypoint movements.
	2. Create [Empty] GameObjects in your scene, these are used as waypoints. It makes things
		easier to manage if you name them waypoint0, waypoint1, etc., and parent them to the 
		[CM_SalsaWaypoints] GameObject.
	3. Set the [Waypoints] [Size] property to match the number of waypoint you created, and link
		waypoint0 to [Waypoints][Element0], waypoint1 to [Waypoints][Element1], etc.
	4. Set the [Triggers] [Size] property to the number of audio clip-based waypoint changes
		you want to make.
	5. Inside each [Triggers] element, you will find the following:
		Trigger: Trigger this waypoint at the [Start] or [End] of an audio clip.
		Audio Clip: The audio clip you want to trigger this waypoint change.
		Movement Speed: How fast you want the target to move.
		Waypoint Index: The waypoint [Element], inside the [Waypoints] array, you want to move towards.
	6. Set the [Target] you want to animate through the waypoints.
	7. The [Starting Waypoint] is a [Waypoints] [Element] index value (Which waypoint to start at)
	8. [Current Waypoint] lets you know, in the inspector, what the current waypoint index is set to.
	9. [Match Waypoint Ration] lets your [Target] move towards the same rotation as the waypoints rotation.
*/

namespace CrazyMinnow.SALSA.Examples
{
	/// <summary>
	/// Properties class for storing waypoint and waypoint trigger information
	/// </summary>
	[System.Serializable]
	public class CM_SalsaWaypointTriggers
	{
		public enum Trigger { Start, End }
		public Trigger trigger = Trigger.Start;
		public AudioClip audioClip;
		public float movementSpeed = 10f; // Movement speed
		public int waypointIndex;
	}

	/// <summary>
	/// A simple waypoints script to allow waypoint movement 
	/// triggered by Salsa_OnTalkStatusChanged events
	/// </summary>
	public class CM_SalsaWaypoints : MonoBehaviour 
	{
		public GameObject target; // The object you wish to move
		public int startingWaypoint; // Current waypoint index
		public int currentWaypoint; // Current waypoint index
		public bool matchWaypointRotation; // Target will match the waypoints rotation
		public CM_SalsaWaypointTriggers[] triggers;
		public GameObject[] waypoints; // Array of waypoints

		private float movementSpeed = 10f; // Movement speed

		private float startTime;
		private float journeyLength;

		/// <summary>
		/// On start, move the tartet to the first waypoint position, 
		/// then set the currentWaypoint to index 1 to being moving 
		/// towards the next waypoint
		/// </summary>
		void Start()
		{
			target.transform.position = waypoints[currentWaypoint].transform.position;
			currentWaypoint = startingWaypoint;
		}

		/// <summary>
		/// Move the target towards the current waypoint index
		/// </summary>
		void Update () 
		{
			target.transform.position = Vector3.MoveTowards(
				target.transform.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * movementSpeed);

			if (matchWaypointRotation)
			{
				target.transform.rotation = Quaternion.RotateTowards( 
			        target.transform.rotation, waypoints[currentWaypoint].transform.rotation, Time.deltaTime * movementSpeed);
			}
		}

		/// <summary>
		/// Method is called by SALSA broadcast when the talk status has changed
		/// </summary>
		/// <param name="status">Status.</param>
		void Salsa_OnTalkStatusChanged(SalsaStatus status)
		{
			for (int i = 0; i < triggers.Length; i++)
			{
				if (triggers[i].trigger == CM_SalsaWaypointTriggers.Trigger.Start && status.isTalking)
				{
					if (triggers[i].audioClip.name == status.clipName)
					{
						movementSpeed = triggers[i].movementSpeed;
						SetSpeed(movementSpeed);
						SetWaypoint(triggers[i].waypointIndex);
					}
				}

				if (triggers[i].trigger == CM_SalsaWaypointTriggers.Trigger.End && !status.isTalking)
				{
					if (triggers[i].audioClip.name == status.clipName)
					{
						movementSpeed = triggers[i].movementSpeed;
						SetSpeed(movementSpeed);
						SetWaypoint(triggers[i].waypointIndex);
					}
				}
			}
		}

		/// <summary>
		/// Set the waypoint index to a valid index in the waypoints array
		/// </summary>
		/// <param name="index">Index.</param>
		public void SetWaypoint(int index)
		{
			if (index < this.waypoints.Length)
			{
				this.currentWaypoint = index;
			}
		}

		/// <summary>
		/// Set the movement speed
		/// </summary>
		/// <param name="speed">Speed.</param>
		public void SetSpeed(float speed)
		{
			this.movementSpeed = speed;
		}

		public void ResetSalsaWaypoints()
		{
			currentWaypoint = startingWaypoint;
			target.transform.position = waypoints[0].transform.position;
		}
	}
}