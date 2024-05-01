using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Disrupt : State
{
	GameObject[] items;
	public int NumberToDisrupt = 3;
	public override void Enter()
	{
		base.Enter();
		items = GameObject.FindGameObjectsWithTag("Light");		//Declare list of objects here
		if (items.Length <= 0)
		{
			Transition(this, "FindState");
			return;
		}
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i].GetComponent<Lights>().isOn == false)
			{
				List<GameObject> tmp = new List<GameObject>(items);
				tmp.RemoveAt(i);
				items = tmp.ToArray();
			}
		}
		for (int i = 0; i < NumberToDisrupt; i++) {disrupt();}
		Transition(this, "FindState");
	}

	void disrupt()
	{
		var i = UnityEngine.Random.Range(0, items.Length);
		if (items[i].GetComponent<Lights>().isOn == true)
		{
			items[i].GetComponent<Lights>().Turnoff();
		}
	}
}
