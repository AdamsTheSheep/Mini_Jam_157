using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Suspicious : State
{
	Timer timer;
	public static Vector3 SoundPosition;
	public override void Enter()
	{
		base.Enter();
		enemyReferences.navMeshAgent.destination = SoundPosition;
		if (timer == null)
		{
			timer = Timer.CreateTimer(gameObject,15,false,true);
			timer.OnTimerEnded += OnTimerEnded;
		}
	}

	public override void StateUpdate()
	{
		base.StateUpdate();
		Debug.Log(Vector3.Distance(enemyReferences.ParentTransform.position,enemyReferences.navMeshAgent.destination));
		if (Vector3.Distance(enemyReferences.ParentTransform.position,enemyReferences.navMeshAgent.destination) < 1)
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
		enemyReferences.navMeshAgent.isStopped = true;
		enemyReferences.navMeshAgent.ResetPath();
		Transition(this, "Chase");
	}

}
