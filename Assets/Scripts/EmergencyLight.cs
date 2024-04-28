using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyLight : MonoBehaviour
{
	public void StartSound()
	{
		GetComponent<SpatializedAudio>().PlayLoop();
	}
}
