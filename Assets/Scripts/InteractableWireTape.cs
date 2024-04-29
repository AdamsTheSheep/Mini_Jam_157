using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWireTape : MonoBehaviour, IInteractable
{
	[SerializeField] AudioClip pickupSound;
	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");
		PlayerUI.instance.CollectWireTape();
		AudioManager.instance.PlayNonSpatializedSFX(pickupSound);
		gameObject.SetActive(false);
	}

	public void StopInteract() { }
}
