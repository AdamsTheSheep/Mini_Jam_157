using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Suspicious : State
{
	Vector3 pos;
	Timer timer;
	public override void Enter()
	{
		base.Enter();
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player == null)
		{
			Transition(this, "FindState");
		}
		pos = player.transform.position;
		enemyReferences.navMeshAgent.destination = pos;
		timer = Timer.CreateTimer(gameObject,15,false,true);
		timer.OnTimerEnded += OnTimerEnded;
	}

	public override void StateUpdate()
	{
		base.StateUpdate();
		if (Vector3.Distance(enemyReferences.ParentTransform.position, pos) < 3)
		{
			enemyReferences.navMeshAgent.isStopped = true;
			timer.isPaused = true;
			Destroy(timer);
			Transition(this, "FindState");
			
		}
	}
	void OnTimerEnded()
	{
		enemyReferences.navMeshAgent.isStopped = true;
		Transition(this, "Chase");
	}

}
