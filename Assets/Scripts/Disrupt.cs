using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Disrupt : State
{
	public override void Enter()
	{
		base.Enter();
		var doors = GameObject.FindGameObjectsWithTag("Door");
		if (doors.Length == 0)
		{
			Transition(this, "FindState");
		}
		var temp = doors;
		foreach (var door in doors)
		{
			if (door.activeSelf == true)
			{
				temp = temp.Where(val => val != door).ToArray();
			}
		}
		doors = temp;
		doors[Random.Range(0, doors.Length)].SetActive(true);
		Transition(this, "FindState");
	}
}
