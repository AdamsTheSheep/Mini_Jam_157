using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSwitch : MonoBehaviour, IInteractable
{

	public virtual void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");
		PlayerUI.PlaySound(gameObject,transform.position);
	}

	public void StopInteract() { }
}
