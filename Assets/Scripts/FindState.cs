using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindState : State
{
	Vector3 RotateTowards;
	Timer timer;
    public override void Enter()
	{
		base.Enter();
		timer = Timer.CreateTimer(gameObject,Random.Range(2,4),false,true);
		timer.OnTimerEnded += OnTimerEnded;
	}

	void OnTimerEnded()
	{
		var rand = Random.Range(0,6);
		switch (rand)
		{
			case 0:
				Transition(this,"Rotate");
				break;
			case 1:
				Transition(this, "Disrupt");
				break;
			default:
				Transition(this,"Roaming");
				break;
		}
	}

	public override void Exit()
	{
		Component.Destroy(timer);
	}
}
