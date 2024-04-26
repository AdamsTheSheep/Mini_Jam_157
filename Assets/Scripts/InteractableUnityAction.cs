using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableUnityAction : MonoBehaviour, IInteractable
{
	[SerializeField] UnityEvent action;

	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");
		action?.Invoke();
	}

	public void StopInteract() { }
}
