using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindState : State
{
	Vector3 RotateTowards;
    public override void Enter()
	{
		base.Enter();
		var timer = Timer.CreateTimer(gameObject,Random.Range(3,6),false,true);
		timer.OnTimerEnded += OnTimerEnded;
	}

	void OnTimerEnded()
	{
		var rand = Random.Range(0,3);
		switch (rand)
		{
			case 0:
				Transition(this,"Rotate");
				break;
			case 1:
				Transition(this,"Roaming");
				break;
			case 2:
				Transition(this, "Disrupt");
				break;
			default:
				Transition(this,"Roaming");
				break;
		}
	}
}