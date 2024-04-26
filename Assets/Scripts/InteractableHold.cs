using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHold : MonoBehaviour, IInteractable
{
	[SerializeField] float holdTime = 2f;

	bool isHolding = false;
	float currentHoldTime = 0f;

	private void Update()
	{
		if (isHolding)
		{
			currentHoldTime += Time.deltaTime;
			PlayerUI.instance.interactableHoldProgressImage.fillAmount = currentHoldTime / holdTime;
		}
	}

	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");
		isHolding = true;
		PlayerUI.instance.interactableHoldProgressImage.fillAmount = 0f;
		PlayerUI.instance.interactableHoldProgressImage.gameObject.SetActive(true);
	}

	public void StopInteract()
	{
		Debug.Log($"Stopped interacting with {gameObject.name} after {currentHoldTime}s / {holdTime}s");
		PlayerUI.instance.interactableHoldProgressImage.gameObject.SetActive(false);
		if (currentHoldTime > holdTime) SuccessfullyHold();
		currentHoldTime = 0f;
	}

	void SuccessfullyHold()
	{
		Destroy(gameObject);
	}
}
