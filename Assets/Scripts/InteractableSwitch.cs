using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSwitch : MonoBehaviour, IInteractable
{
	[SerializeField] GameObject itemToSwitch;

	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");
		itemToSwitch.SetActive(!itemToSwitch.activeSelf);
	}

	public void StopInteract() { }
}
