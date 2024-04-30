using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lights : MonoBehaviour
{
	public float OffTime = 20;
	public bool CanTurnOn = true;

	public void Turnoff()
	{
		if (transform.GetChild(0).gameObject.activeSelf == false)
		{
			var timer = Timer.CreateTimer(transform.parent.gameObject, OffTime, false,true);
			timer.OnTimerEnded += OnTimerEnded;
			transform.GetChild(0).gameObject.SetActive(true);
			GetComponent<NavMeshObstacle>().enabled = false;
		}
	}

	void OnTimerEnded()
	{
		if (CanTurnOn) {gameObject.GetComponent<Animation>().Play(); GetComponent<NavMeshObstacle>().enabled = true;}
		
	}
}
