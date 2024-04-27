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
		GameObject[] items = new List<GameObject>().ToArray();		//Declare list of objects here
		if (items.Length == 0)
		{
			Transition(this, "FindState");
			return;
		}
																	//Do stuffs with list here
		Transition(this, "FindState");
	}
}
