using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GlobalState : State
{
	public float VisionRangeAngle = 55;
	public float VisionDistance = 5;
	int anger;
	int AngerLevel
	{
		set {setAnger(value);}
		get {return Math.Clamp(anger,0,4);}
	}
	[SerializeField] Timer timer;
	StateMachine stateMachine;
	public static GameManager.SuspicionEvent TriggerSuspicion;
	void Start()
	{
		stateMachine = gameObject.GetComponent<StateMachine>();
		TriggerSuspicion += trigger;
		AngerLevel = 0;
		timer.OnTimerEnded += OnTimerEnded;
	}
	public override void StateUpdate()
	{
		Debug.Log(AngerLevel + " - " + stateMachine.CurrentState);
		var spottedPlayer = Vision();
		base.StateUpdate();
		if (spottedPlayer && stateMachine.CurrentState.GetType() != typeof(Chase))		//Condition for chase state
		{
			Transition(stateMachine.CurrentState, "Chase");
			AngerLevel = 4;
		}
	}

	void setAnger(int value)
	{
		anger = Math.Clamp(value,0,4);
		if (timer.isPaused && AngerLevel > 0)
		{
			timer.Begin();
		}
	}

	void OnTimerEnded()
	{
		AngerLevel -= 1;
	}

	void trigger(int Level, Vector3 pos)
	{
		AngerLevel += Level;
		if (stateMachine.CurrentState.GetType() != typeof(Chase))
		{
			switch (AngerLevel)
			{
				case 0:
					break;
				case 1:
					Transition(stateMachine.CurrentState, "Rotate");
					break;
				case 2: case 3:
					Suspicious.SoundPosition = pos;
					Transition(stateMachine.CurrentState, "Suspicious");
					break;
				case var x when x > 3:
					Transition(stateMachine.CurrentState, "Chase");
					break;
			}
		}
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
