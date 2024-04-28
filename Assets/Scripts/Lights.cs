using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
	public float OffTime = 20;
	public void Turnoff()
	{
		if (gameObject.activeSelf == false)
		{
			var timer = Timer.CreateTimer(transform.parent.gameObject, OffTime, false,true);
			timer.OnTimerEnded += OnTimerEnded;
			gameObject.SetActive(false);
		}
	}

	void OnTimerEnded()
	{
		gameObject.SetActive(true);
	}
}
