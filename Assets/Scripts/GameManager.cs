using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
	public delegate void Event();
	public delegate void SuspicionEvent(int ImpactLevel, Vector3 Location);
	public delegate void StateEvent(State currentstate, string NextState);

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
}
