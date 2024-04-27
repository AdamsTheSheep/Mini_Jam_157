using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWireTape : MonoBehaviour, IInteractable
{
	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");
		PlayerUI.instance.CollectWireTape();
		gameObject.SetActive(false);
	}

	public void StopInteract() { }
}
