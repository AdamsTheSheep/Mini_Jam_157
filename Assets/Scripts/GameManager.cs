using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class GameManager
{
	public delegate void Event();
	public delegate void SuspicionEvent(int ImpactLevel, Vector3 Location);
	public delegate void StateEvent(State currentstate, string NextState);

	public static GameManager.Event OpenDoors;

	public static bool usingGenerator;
	public static int count = 3;
	public static int objectiveCount
	{
		get { return GameManager.count; }
		set
		{
			count = value;
			CheckAllFixed(value);
		}
	}
	private static bool playerHasWireTape;

	public static void ResetStaticValues()
	{
		usingGenerator = false;
		playerHasWireTape = false;
		count = 3;
	}

	public static void CheckAllFixed(int value)
    {
		if (value < objectiveCount) GameManager.CloseDoor();
        if (objectiveCount <= 0)
        {
            if (OpenDoors != null) OpenDoors();
        }
    }

	public static bool PlayerHasWireTape
	{
		get => playerHasWireTape; 
		set
		{
			playerHasWireTape = value;
			PlayerUI.instance.UpdateWireTapeUI();
		}
	}

	public static void CloseDoor()
	{
		Doors[] doors = GameObject.FindObjectsByType<Doors>(FindObjectsSortMode.None);
			Debug.Log($"Found {doors.Count()} doors");
		Doors[] closables = doors.Where(x => x.isOpen && x.canMonsterInteract).ToArray();
			Debug.Log($"{closables.Count()} doors are closable");
		if (closables.Length == 0)
		{
			Debug.Log("no doors available to close");
			return;
		}

		var chosen = closables[Random.Range(0,closables.Length)].GetComponent<Doors>();
		chosen.Close();
			Debug.Log($"Closed {chosen.gameObject.name}");
	}
}
