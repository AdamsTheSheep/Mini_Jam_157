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

	public static bool usingGenerator;
	public static bool playerHasFixedGenerator;
	public static bool playerHasFixedWires;
	public static bool playerHasFixedSwitch;
	public static bool playerHasFixedAll;
	private static bool playerHasWireTape;

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
