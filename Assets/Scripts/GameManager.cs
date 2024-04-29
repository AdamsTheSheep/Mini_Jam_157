using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class GameManager
{
	public delegate void Event();
	public delegate void SuspicionEvent(int ImpactLevel, Vector3 Location);
	public delegate void StateEvent(State currentstate, string NextState);

	public static GameManager.Event OpenDoors;

	public static bool usingGenerator;
	public static int count;
	public static int objectiveCount
	{
		set { CheckAllFixed(value); }
		get {return GameManager.count;}
	}
	private static bool playerHasWireTape;

	public static void CheckAllFixed(int value)
    {
		GameManager.count = value;
        if (GameManager.objectiveCount <= 0)
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
		GameObject[] closables = new List<GameObject>().ToArray();
		var doors = GameObject.FindGameObjectsWithTag("Door");
		foreach (var door in doors)
		{
			if (door.GetComponent<Doors>().isOpen == true)
			{
				closables = closables.Append(door).ToArray();
			}
		}
		var chosen = closables[Random.Range(0,closables.Length)].GetComponent<Doors>();
		chosen.Close();
	}
}
