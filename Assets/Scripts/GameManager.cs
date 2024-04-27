using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
	public delegate void Event();

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
