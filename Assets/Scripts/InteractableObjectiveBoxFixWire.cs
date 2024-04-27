using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectiveBoxFixWire : MonoBehaviour, IInteractable
{
	[SerializeField] float holdTime = 5f;
	[SerializeField] string alreadyFixedMessage;
	[SerializeField] string holdInteractionText;
	[SerializeField] string holdInteractionFinishedText;

	[SerializeField] bool isFixed = false;

	Timer timer;

	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");

		if (isFixed)
		{
			PlayerUI.instance.holdInteractionText.gameObject.SetActive(true);
			PlayerUI.instance.holdInteractionText.text = alreadyFixedMessage;
			return;
		}

		if (!GameManager.PlayerHasWireTape)
		{
			Debug.Log($"Player does not have required wire tape");
			PlayerUI.instance.ShowWireTapeMissingMessage();
			return;
		}

		PlayerUI.instance.interactableHoldProgressImage.fillAmount = 0f;
		PlayerUI.instance.interactableHoldProgressImage.gameObject.SetActive(true);

		if (!string.IsNullOrEmpty(holdInteractionText))
		{
			PlayerUI.instance.holdInteractionText.gameObject.SetActive(true);
			PlayerUI.instance.holdInteractionText.text = holdInteractionText;
		}

		timer = Timer.CreateTimer(gameObject, 5f, false, true);
		timer.OnTimerUpdated += (currentTime) =>
		{
			PlayerUI.instance.interactableHoldProgressImage.fillAmount = currentTime / holdTime;
		};
		timer.OnTimerEnded += () =>
		{
			SuccessfullyHold();
		};
	}

	public void StopInteract()
	{
		Debug.Log($"Stopped interacting with {gameObject.name}");

		PlayerUI.instance.interactableHoldProgressImage.gameObject.SetActive(false);
		if (GameManager.PlayerHasWireTape || isFixed) PlayerUI.instance.holdInteractionText.gameObject.SetActive(false);
	}

	void SuccessfullyHold()
	{
		print($"{gameObject.name} successfully held");
		PlayerUI.instance.holdInteractionText.text = holdInteractionFinishedText;
		isFixed = true;
		//TODO : set objective as completed, repair ligths etc
	}

	void Break()
	{
		isFixed = false;
		//TODO : alert player?
	}
}
