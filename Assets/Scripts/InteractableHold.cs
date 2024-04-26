using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHold : MonoBehaviour, IInteractable
{
	[SerializeField] float holdTime = 2f;
	[SerializeField] string holdInteractionText;
	[SerializeField] string holdInteractionFinishedText;

	bool isHolding = false;
	float currentHoldTime = 0f;

	private void Update()
	{
		if (isHolding)
		{
			currentHoldTime += Time.deltaTime;
			PlayerUI.instance.interactableHoldProgressImage.fillAmount = currentHoldTime / holdTime;
			if (currentHoldTime > holdTime) PlayerUI.instance.holdInteractionText.text = holdInteractionFinishedText;
		}
	}

	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");
		isHolding = true;
		PlayerUI.instance.interactableHoldProgressImage.fillAmount = 0f;
		PlayerUI.instance.interactableHoldProgressImage.gameObject.SetActive(true);
		if (!string.IsNullOrEmpty(holdInteractionText))
		{
			PlayerUI.instance.holdInteractionText.gameObject.SetActive(true);
			PlayerUI.instance.holdInteractionText.text = holdInteractionText;
		}
	}

	public void StopInteract()
	{
		Debug.Log($"Stopped interacting with {gameObject.name} after {currentHoldTime}s / {holdTime}s");
		PlayerUI.instance.interactableHoldProgressImage.gameObject.SetActive(false);
		if (currentHoldTime > holdTime) SuccessfullyHold();
		currentHoldTime = 0f;
		PlayerUI.instance.holdInteractionText.gameObject.SetActive(false);
	}

	void SuccessfullyHold()
	{
		Destroy(gameObject);
	}
}
