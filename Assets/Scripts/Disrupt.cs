using System;
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
		GameObject[] items = GameObject.FindGameObjectsWithTag("Light");		//Declare list of objects here
		if (items.Length == 0)
		{
			Transition(this, "FindState");
			return;
		}
		var i = UnityEngine.Random.Range(0, items.Length);
		if (items[i].activeSelf == true)
		{
			items[i].GetComponent<Lights>().Turnoff();
		}
		Transition(this, "FindState");
	}
}
