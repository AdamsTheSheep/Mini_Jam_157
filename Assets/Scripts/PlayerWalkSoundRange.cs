using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWalkSoundRange : MonoBehaviour
{
	public FPSController controller;
	bool CanSendSound = true;

    void Update()
	{
		if((controller.moveDirection.x != 0 || controller.moveDirection.z != 0) && CanSendSound)
		{
			ActivateSound();
		}
	}

	void ActivateSound()
	{
		if (GetComponent<AudioSource>().isPlaying == false) GetComponent<SpatializedAudio>().PlaySound();
		var colliders = Physics.OverlapBox(transform.position,transform.localScale / 2);
		foreach (var collider in colliders)
		{
			if (collider.gameObject.tag == "Enemy")
			{
				Debug.Log("Triggeded");
				GlobalState.TriggerSuspicion(1, transform.parent.position);
				var timer = Timer.CreateTimer(gameObject, 5,false, true);
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
