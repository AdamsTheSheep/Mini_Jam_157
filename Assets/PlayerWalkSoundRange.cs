using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkSoundRange : MonoBehaviour
{
    void Update()
	{
		var colliders = Physics.OverlapBox(transform.position,transform.localScale / 2);
		foreach (var collider in colliders)
		{
			if (collider.gameObject.tag == "Enemy")
			{
				Debug.Log("A");
				GlobalState.TriggerSuspicion();
			}
		}
	}
}
