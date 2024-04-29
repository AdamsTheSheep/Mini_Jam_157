using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
	GameObject player;
	Timer timer;
	Timer LightsOffTimer;
	public float ChaseDistance;
	public float ChaseSpeed;
	public override void Enter()
	{
		base.Enter();
		enemyReferences.navMeshAgent.speed = ChaseSpeed;
		player = GameObject.FindGameObjectWithTag("Player");
		transform.LookAt(player.transform);
		timer = Timer.CreateTimer(gameObject, 5, true, false);
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
		var lights = GameObject.FindGameObjectsWithTag("Light");
		foreach (var light in lights)
		{
			if (light.activeSelf == true)
			{
				light.GetComponent<Lights>().Turnoff();
			}
		}
		base.StateUpdate();
		enemyReferences.navMeshAgent.destination = player.transform.position;
	}

	public override void Exit()
	{
		base.Exit();
		gameObject.GetComponent<GlobalState>().setAnger(1);
		enemyReferences.navMeshAgent.isStopped = true;
		enemyReferences.navMeshAgent.ResetPath();
		Component.Destroy(timer);
	}
}
