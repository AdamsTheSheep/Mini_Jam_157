using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Roaming : State
{
	public float RoamingTime;
	Timer timer;
	public override void Enter()
	{
		base.Enter();
		if (GameObject.FindGameObjectWithTag("NavSurface") == null) {Debug.Log("There's no game object with tag \"NavSurface\""); return;}
		var surface = GameObject.FindGameObjectWithTag("NavSurface").GetComponent<NavMeshSurface>();
        Vector3 target = SetRandomDest(surface.navMeshData.sourceBounds);
		enemyReferences.navMeshAgent.SetDestination(target);
		timer = Timer.CreateTimer(gameObject, RoamingTime,false,true);
		timer.OnTimerEnded += OnTimerEnded;
	}

	private void OnTimerEnded()
	{
		Transition(this,"FindState");
	}

	public override void StateUpdate()
	{
		Debug.Log(enemyReferences.navMeshAgent.destination);
		if (enemyReferences.navMeshAgent.hasPath == false)
		{
			Transition(this,"FindState");
		}
	}

	public override void Exit()
	{
		base.Exit();
		enemyReferences.navMeshAgent.isStopped = true;
		enemyReferences.navMeshAgent.ResetPath();
	}

	Vector3 SetRandomDest(Bounds bounds)
    {
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var z = Random.Range(bounds.min.z, bounds.max.z);
     
        var destination = new Vector3(x,1, z);
        return destination;
    }
}
