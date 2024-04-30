using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
	public float OffTime = 20;

	public void Turnoff()
	{
		if (transform.GetChild(0).gameObject.activeSelf == false)
		{
			var timer = Timer.CreateTimer(transform.parent.gameObject, OffTime, false,true);
			timer.OnTimerEnded += OnTimerEnded;
			transform.GetChild(0).gameObject.SetActive(true);
		}
	}

	void OnTimerEnded()
	{
		gameObject.GetComponent<Animation>().Play();
	}
}
