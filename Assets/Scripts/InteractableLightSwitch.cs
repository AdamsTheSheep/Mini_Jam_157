using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLightSwitch : InteractableSwitch
{
	bool State = true;
	public GameObject[] Lights;
    public override void Interact()
	{
		base.Interact();
		State = !State;
		foreach (var light in Lights)
		{
			if (light.GetComponent<Lights>() == null) continue;
			if (!State)
			{
				light.GetComponent<Lights>().Turnoff();
			}
			else light.GetComponent<Lights>().TurnOn();
		}
		var i = (State) ? 1 : 2;
		GetComponent<SpatializedAudio>().PlaySound(i);
	}
}
