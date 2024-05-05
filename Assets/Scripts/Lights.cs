using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Lights : MonoBehaviour
{
	public float OffTime = 20;
	public bool CanTurnOn = true;
	public bool isOn = true;
	Timer timer;
	public void Turnoff()
	{
		if (transform.GetChild(0).gameObject.activeSelf == false)
		{
			timer = Timer.CreateTimer(transform.parent.gameObject, OffTime, false,true);
			timer.OnTimerEnded += OnTimerEnded;
			transform.GetChild(0).gameObject.SetActive(true);
			GetComponent<NavMeshObstacle>().enabled = false;
			GetComponent<Light>().intensity = 0;
			isOn = false;
		}
	}

	public void TurnOn()
	{
		if (timer != null && !timer.IsDestroyed()) Component.Destroy(timer);
		OnTimerEnded();
		
	}

	void OnTimerEnded()
	{
		if (CanTurnOn && !isOn) {gameObject.GetComponent<Animation>().Play(); GetComponent<NavMeshObstacle>().enabled = true; isOn = true;}
	}
}
