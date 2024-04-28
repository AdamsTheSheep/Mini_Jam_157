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
		enemyReferences.navMeshAgent.destination = player.transform.position;
		if (enemyReferences.navMeshAgent.remainingDistance > ChaseDistance)
		{
			Transition(this, "FindState");
		}
	}

	public override void StateUpdate()
	{
		base.StateUpdate();
		if (enemyReferences.navMeshAgent.hasPath == false)
		{
			OnTimerEnded();
		}
	}

	public override void Exit()
	{
		base.Exit();
		enemyReferences.navMeshAgent.isStopped = true;
		enemyReferences.navMeshAgent.ResetPath();
		Destroy(timer);
	}
}
