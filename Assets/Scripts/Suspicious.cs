using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Suspicious : State
{
	MonsterAudio monsterAudio;
	Timer timer;
	public static Vector3 SoundPosition;
	public float Speed;
	Coroutine idleTimerCoroutine;
	
	private void Awake()
	{
		monsterAudio = GetComponent<MonsterAudio>();
	}
	public override void Enter()
	{
		base.Enter();
		InvokeRepeating("MonsterAudioPlaySteps", 0, .562f);
		idleTimerCoroutine = StartCoroutine(MonsterAudioPlayIdle());
		enemyReferences.navMeshAgent.speed = Speed;
		enemyReferences.navMeshAgent.destination = SoundPosition;
		if (timer == null)
		{
			timer = Timer.CreateTimer(gameObject,20,false,true);
			timer.OnTimerEnded += OnTimerEnded;
		}
	}

	void MonsterAudioPlaySteps()
	{
		monsterAudio.PlaySteps();
	}

	IEnumerator MonsterAudioPlayIdle()
	{
		yield return new WaitForSeconds(Random.Range(3f, 10f));
		monsterAudio.PlayIdle();
		idleTimerCoroutine = StartCoroutine(MonsterAudioPlayIdle());
	}

	public override void StateUpdate()
	{
		base.StateUpdate();
		if (Vector3.Distance(enemyReferences.ParentTransform.position, SoundPosition) < 1)
		{
			enemyReferences.navMeshAgent.isStopped = true;
			enemyReferences.navMeshAgent.ResetPath();
			timer.isPaused = true;
			Component.Destroy(timer);
			Transition(this, "FindState");
			
		}
	}
	void OnTimerEnded()
	{
		Transition(this, "FindState");
	}

	public override void Exit()
	{
		base.Exit();
		enemyReferences.navMeshAgent.isStopped = true;
		enemyReferences.navMeshAgent.ResetPath();
		StopCoroutine(idleTimerCoroutine);
		CancelInvoke();
		Component.Destroy(timer);

	}

}
