using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : State
{
	StateMachine stateMachine;

	void Start()
	{
		stateMachine = gameObject.GetComponent<StateMachine>();
	}
	public override void StateUpdate()
	{
		var temp = 0;
		base.StateUpdate();
		if (temp != 0 && stateMachine.CurrentState.GetType() != typeof(Chase))		//Condition for chase state
		{
			Transition(stateMachine.CurrentState, "Chase");
		}
	}
}
