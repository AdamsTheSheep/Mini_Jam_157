using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GlobalState : State
{
	public float VisionRangeAngle = 55;
	public float VisionDistance = 5;
	public int anger;
	public int AngerLevel
	{
		set {setAnger(value);}
		get {return Math.Clamp(anger,0,5);}
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
		var spottedPlayer = Vision();
		base.StateUpdate();
		if (spottedPlayer && stateMachine.CurrentState.GetType() != typeof(Chase))		//Condition for chase state
		{
			Transition(stateMachine.CurrentState, "Chase");
			AngerLevel = 4;
		}
	}

	public void setAnger(int value)
	{
		anger = Math.Clamp(value,0,5);
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
		if (stateMachine.CurrentState.GetType() != typeof(Chase))
		{
			AngerLevel += Level;
			Debug.Log("Anger Level: " + AngerLevel);
			switch (AngerLevel)
			{
				case 0:
					break;
				case 1:
					Transition(stateMachine.CurrentState, "Rotate");
					break;
				case var x when (x > 3 && Vector3.Distance(enemyReferences.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 7):
					Transition(stateMachine.CurrentState, "Chase");
					break;
				default:
					Suspicious.SoundPosition = pos;
					Transition(stateMachine.CurrentState, "Suspicious");
					break;
			}
		}
	}

	bool Vision()
	{
		for (float i = -VisionRangeAngle / 2; i < VisionRangeAngle / 2; i++)
		{
			RaycastHit ray;
			Physics.Raycast(transform.position,Quaternion.AngleAxis(i, Vector3.up) * transform.forward,out ray, VisionDistance);
			Debug.DrawRay(transform.position,Quaternion.AngleAxis(i, Vector3.up) * transform.forward * VisionDistance, Color.red);
			if (ray.collider && ray.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				return true;
			}
		}
		return false;
	}
}
