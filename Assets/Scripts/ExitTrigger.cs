using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		PlayerUI.instance.ShowWinScreen();
	}
}
