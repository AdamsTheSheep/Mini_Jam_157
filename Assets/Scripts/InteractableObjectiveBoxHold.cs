using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectiveBoxHold : MonoBehaviour, IInteractable
{
	[SerializeField] float holdTime = 5f;
	[SerializeField] string holdInteractionText;
	[SerializeField] string holdInteractionFinishedText;

	Timer timer;

	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");

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
			PlayerUI.instance.holdInteractionText.text = holdInteractionFinishedText;
		};
	}

	public void StopInteract()
	{
		Debug.Log($"Stopped interacting with {gameObject.name}");

		timer.isPaused = true;
		Destroy(timer);
		PlayerUI.instance.interactableHoldProgressImage.gameObject.SetActive(false);
		PlayerUI.instance.holdInteractionText.gameObject.SetActive(false);
	}

	void SuccessfullyHold()
	{
		print($"{gameObject.name} successfully held");
		PlayerUI.instance.FixSwitch();
		//TODO : set objective as completed, repair ligths etc
	}
}
