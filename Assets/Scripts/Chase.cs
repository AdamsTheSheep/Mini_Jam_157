using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
	GameObject player;
	Timer timer;
	public float ChaseDistance;
	public float ChaseSpeed;
	bool CanAttack;
	Timer AttackCooldown;
	public override void Enter()
	{
		base.Enter();
		CanAttack = true;
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
		base.StateUpdate();
		var lights = GameObject.FindGameObjectsWithTag("Light");
		foreach (var light in lights)
		{
			if (light.activeSelf == true)
			{
				light.GetComponent<Lights>().Turnoff();
			}
		}

		if (Vector3.Distance(enemyReferences.ParentTransform.position, player.transform.position) < 3 && CanAttack)
		{
			enemyReferences.animator.speed = 1f;
			enemyReferences.animator.SetInteger("CurrentState", ((int)EntityAnimController.States.Attack));
			CanAttack = false;
			AttackCooldown = Timer.CreateTimer(gameObject,3,false,true);
			AttackCooldown.OnTimerEnded += OnCoolDownEnded;
		}

		if (EntityAnimController.isAttacking && Vector3.Distance(enemyReferences.transform.position, player.transform.position) < 2)
		{
			PlayerUI.GameLost();
		}
		enemyReferences.navMeshAgent.destination = player.transform.position;
	}

	void OnCoolDownEnded()
	{
		CanAttack = true;
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
