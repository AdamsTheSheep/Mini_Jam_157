using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : State
{
	StateMachine stateMachine;
	public static GameManager.Event TriggerSuspicion; 
	void Start()
	{
		stateMachine = gameObject.GetComponent<StateMachine>();
		TriggerSuspicion += trigger;
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

	void trigger()
	{
		Transition(stateMachine.CurrentState, "Suspicious");
	}

}
