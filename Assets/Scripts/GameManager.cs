using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
	public delegate void Event();

	public delegate void StateEvent(State currentstate, string NextState);

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
