using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : State
{
	public float VisionRangeAngle = 55;
	public float VisionDistance = 5;
	StateMachine stateMachine;
	public static GameManager.Event TriggerSuspicion;
	void Start()
	{
		stateMachine = gameObject.GetComponent<StateMachine>();
		TriggerSuspicion += trigger;
	}
	public override void StateUpdate()
	{
		var spottedPlayer = Vision();
		base.StateUpdate();
		if (spottedPlayer && stateMachine.CurrentState.GetType() != typeof(Chase))		//Condition for chase state
		{
			Transition(stateMachine.CurrentState, "Chase");
		}
	}

	void trigger()
	{
		Transition(stateMachine.CurrentState, "Suspicious");
	}

	bool Vision()
	{
		Collider[] cols = Physics.OverlapSphere(transform.position, VisionDistance);
		Vector3 characterToCollider;
		float dot;
		foreach (Collider collider in cols)
		{
			characterToCollider = (collider.transform.position-transform.position).normalized;
			dot = Vector3.Dot(characterToCollider, transform.forward);
			if (dot >= Mathf.Cos(VisionRangeAngle) && collider.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				return true;
			}
		}
		return false;
	}
}
