using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
	MonsterAudio monsterAudio;
	GameObject player;
	Timer timer;
	public float ChaseDistance;
	public float ChaseSpeed;
	public float AttackDistance = 4;
	bool CanAttack;
	Timer AttackCooldown;

	private void Awake()
	{
		monsterAudio = GetComponent<MonsterAudio>();
	}

	public override void Enter()
	{
		base.Enter();
		monsterAudio.PlayRage();
		monsterAudio.PlayLoop();
		InvokeRepeating("MonsterAudioPlaySteps", 0, .4f);
		CanAttack = true;
		enemyReferences.navMeshAgent.speed = ChaseSpeed;
		player = GameObject.FindGameObjectWithTag("Player");
		transform.LookAt(player.transform);
		timer = Timer.CreateTimer(gameObject, 5, true, false);
		timer.OnTimerEnded += OnTimerEnded;
	}

	void MonsterAudioPlaySteps()
	{
		monsterAudio.PlaySteps();
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
		base.StateFixedUpdate();
		var lights = GameObject.FindGameObjectsWithTag("Light");
		foreach (var light in lights)
		{
			if (light.activeSelf == true)
			{
				light.GetComponent<Lights>().Turnoff();
			}
		}

		if ((Vision(AttackDistance) && CanAttack) || (CanAttack && Vector3.Distance(enemyReferences.transform.position,player.transform.position) <= 0.4))
		{
			enemyReferences.animator.speed = 1f;
			enemyReferences.animator.SetInteger("CurrentState", ((int)EntityAnimController.States.Attack));
			CanAttack = false;
			monsterAudio.PlayAttack();
			AttackCooldown = Timer.CreateTimer(gameObject,3,false,true);
			AttackCooldown.OnTimerEnded += OnCoolDownEnded;
		}

		if (EntityAnimController.isAttacking && Vision(AttackDistance - 1)|| (EntityAnimController.isAttacking && Vector3.Distance(enemyReferences.transform.position,player.transform.position) <= 0.4))
		{
			GameObject.FindAnyObjectByType<PlayerUI>().GameLost();
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
		monsterAudio.StopLoop();
		CancelInvoke();
		BreakLight();
		Component.Destroy(timer);
	}

	void BreakLight()
	{
		var lights = GameObject.FindGameObjectsWithTag("Light");
		var rand = Random.Range(0, lights.Length);
		if (lights[rand].GetComponent<Lights>().CanTurnOn == false)
		{
			BreakLight();
			return;
		}
		lights[rand].GetComponent<Lights>().Turnoff();
		lights[rand].GetComponent<Lights>().CanTurnOn = false;
	}

	bool Vision(float distance)
	{
		for (float i = -180 / 2; i < 180 / 2; i++)
		{
			RaycastHit ray;
			Physics.Raycast(transform.position,Quaternion.AngleAxis(i, Vector3.up) * transform.forward,out ray, distance);
			Debug.DrawRay(transform.position,Quaternion.AngleAxis(i, Vector3.up) * transform.forward * distance, Color.yellow);
			if (ray.collider && ray.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				return true;
			}
		}
		return false;
	}
}
