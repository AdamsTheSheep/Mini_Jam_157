using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
	[SerializeField] List<State> States;
	[SerializeField] State GlobalState;
	[SerializeField] State InitialState;
	[SerializeField] EnemyReferences enemyReferences;
    [HideInInspector] public State CurrentState;

	protected void Start()
	{
		foreach (State state in transform.GetComponents<State>()) {States.Add(state);}	//find and append all states found in the state machine's transform
		if (States.Count < 1) {return;}													//check if there is any state in the state list
		foreach (State state in States)
		{
			state.Transition += Transition;				//connect each state transition delegate to the machine's method to transition
			state.enemyReferences = enemyReferences;
		}
		if (!InitialState) {InitialState = States[0];}									//assign an initial state if there is none
		CurrentState = InitialState;
		CurrentState.Enter();
	}

	protected void Update()
	{
		if (GlobalState) {GlobalState.StateUpdate();}
		if (CurrentState) {CurrentState.StateUpdate();}
    }

	void FixedUpdate()
	{
		if (GlobalState) {GlobalState.StateFixedUpdate();}
        if (CurrentState) {CurrentState.StateFixedUpdate();}
	}

    void Transition(State currentstate, string NextState)
	{
		var temp = false;
		State nextstate = null;
		foreach (State state in States)	//Check whether the state to transition to is in list
		{
			if (state.GetType().ToString() == NextState) 
			{
				temp = true;
				nextstate = state;
				break;
			}
		}

// --------------------------------- Invalid calls check------------------------------------------
		if (!temp)
		{
			Debug.LogError("The state to transition to (" + NextState.GetType() + ") does not exist in the state machine's transform");
			return;
		}
		if (currentstate.GetType().ToString() != CurrentState.GetType().ToString())
		{
			Debug.LogError("Invalid transition call of the state \"" + currentstate.GetType() + "\". (The current state is " + CurrentState.GetType() + ").");
			return;
		}
// --------------------------------- End of Invalid calls check-----------------------------------
		CurrentState.Exit();
		CurrentState = nextstate;
		CurrentState.Enter();
		Debug.Log(CurrentState);
	}
}

