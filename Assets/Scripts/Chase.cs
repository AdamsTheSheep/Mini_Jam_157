using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
	GameObject player;
	Timer timer;
	public float ChaseDistance;
	public override void Enter()
	{
		base.Enter();
		player = GameObject.FindGameObjectWithTag("Player");
		transform.LookAt(player.transform);
		timer = Timer.CreateTimer(gameObject, 3, true, false);
		timer.OnTimerEnded += OnTimerEnded;
	}

	void OnTimerEnded()
	{
		if (Vector3.Distance(enemyReferences.ParentTransform.position, player.transform.position) > ChaseDistance)
		{
			Transition(this, "FindState");
		}
	}

	public override void StateUpdate()
	{
		base.StateUpdate();
		enemyReferences.navMeshAgent.destination = player.transform.position;
	}

	public override void Exit()
	{
		base.Exit();
		enemyReferences.navMeshAgent.isStopped = true;
		enemyReferences.navMeshAgent.ResetPath();
		Component.Destroy(timer);
	}
}
