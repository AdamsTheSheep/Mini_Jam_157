using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWalkSoundRange : MonoBehaviour
{
	[SerializeField] float timeBetweenSteps = 1f;
	[SerializeField] float timeBetweenStepsRun = .7f;

	new SpatializedAudio audio;
	public FPSController controller;
	bool CanSendSound = true;
	bool playingWalk;
	bool playingRun;

	private void Awake()
	{
		audio = GetComponent<SpatializedAudio>();
	}

	void Update()
	{
		if(!playingRun && controller.isRunning && (controller.moveDirection.x != 0 || controller.moveDirection.z != 0))
		{
			playingRun = true;
			playingWalk = false;
			CancelInvoke();
			InvokeRepeating("PlayStepSound", 0f, timeBetweenStepsRun);
		}
		else if(!playingWalk && !controller.isRunning && (controller.moveDirection.x != 0 || controller.moveDirection.z != 0))
		{
			playingRun = false;
			playingWalk = true;
			CancelInvoke();
			InvokeRepeating("PlayStepSound", 0f, timeBetweenSteps);
		}
		else if((controller.moveDirection.x == 0 && controller.moveDirection.z == 0))
		{
			playingRun = false;
			playingWalk = false;
			CancelInvoke();
		}
		if(CanSendSound && (playingWalk || playingRun))
		{
			ActivateSound();
		}
	}

	void PlayStepSound()
	{
		audio.PlaySound();
	}

	//if enemy suspicion triggered, do not trigger again before 5 seconds
	void ActivateSound()
	{
		var colliders = Physics.OverlapBox(transform.position,transform.localScale / 2);
		foreach (var collider in colliders)
		{
			if (collider.gameObject.tag == "Enemy")
			{
				GlobalState.TriggerSuspicion(1, transform.parent.position);
				var timer = Timer.CreateTimer(gameObject, 4,false, true);
				timer.OnTimerEnded += OnTimerEnded;
				CanSendSound = false;
				return;
			}
		}
	}

	void OnTimerEnded()
	{
		CanSendSound = true;
	}
}
