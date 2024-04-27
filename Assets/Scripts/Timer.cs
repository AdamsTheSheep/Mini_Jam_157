using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

// NOTE: 	You can create a new timer in script, use the static CreateTimer method.
//			This timer is meant to use for counting milliseconds, although it technically works for smaller time value if not paused
//			The timeRemaining variable will be updated each second(unless timeRemaining is less than 1, then it will update after the remaining amount of time)
//			If paused while the time is not rounded, the remaining time will be rounded up (EX: 5.672 --> 6; 0.2 --> 1)

public class Timer : MonoBehaviour
{
	public float duration = 60;
	public float timeRemaining;
	public bool LoopTimer = false;
	public bool StartOnBegin;
	public bool destroyOnEnd;
	public GameManager.Event OnTimerEnded;
	public Action<float> OnTimerUpdated;

	private bool isCountingDown;

	public bool isPaused
	{
		get { return !isCountingDown; }
		set { SetPause(value); }
	}

	void Start()
	{
		if (StartOnBegin) { Begin(); }
	}

	public void Begin()
	{
		if (isPaused)
		{
			isPaused = false;
			duration = Mathf.Clamp(duration, 0f, 86400f);
			timeRemaining = duration;
			if (duration < 1) { Invoke("_tick", duration); }
			else Invoke("_tick", 0.1f);
		}
	}

	private void _tick()
	{
		if (isPaused == true) { return; }

		timeRemaining -= 0.1f;
		OnTimerUpdated?.Invoke(timeRemaining);

		if (timeRemaining > 0.1) { Invoke("_tick", 0.1f); }
		else if (timeRemaining > 0) { Invoke("_tick", timeRemaining); }
		else
		{
			isPaused = true;
			if (OnTimerEnded != null) { OnTimerEnded(); }
			if (destroyOnEnd == true) { Destroy(this); }
			if (LoopTimer == true) { Begin(); }
		}
	}

	private void SetPause(bool value)
	{
		isCountingDown = !value;
		if (value == false && timeRemaining > 0) { Invoke("_tick", 0.1f); }
	}

	public static Timer CreateTimer(GameObject ObjectToAttach, float Duration, bool looptimer, bool DestroyOnEnd, bool autoStart = true)
	{
		if (!ObjectToAttach) { Debug.LogError("Cannot find the required component to attach timer to."); return null; }
		var timer = ObjectToAttach.AddComponent<Timer>();
		timer.duration = Duration;
		timer.destroyOnEnd = DestroyOnEnd;
		timer.StartOnBegin = autoStart;
		return timer;
	}
}