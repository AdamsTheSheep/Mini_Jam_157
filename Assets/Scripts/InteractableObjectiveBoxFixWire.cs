using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpatializedAudio))]
public class InteractableObjectiveBoxFixWire : MonoBehaviour, IInteractable
{
	[SerializeField] float holdTime = 5f;
	[SerializeField] string alreadyFixedMessage;
	[SerializeField] string holdInteractionText;
	[SerializeField] string holdInteractionFinishedText;

	[SerializeField] bool isFixed = false;

	[SerializeField] float nextRandomSoundTime;
	[SerializeField] float minTimeBetweenRandomSounds;
	[SerializeField] float maxTimeBetweenRandomSounds;

	Coroutine randomSfxCoroutine;
	Timer timer;


	private void Start()
	{
		randomSfxCoroutine = StartCoroutine(PlayAmbianceAtRandomTime());
	}

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
		GetComponent<SpatializedAudio>().PlaySound();

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

		if (timer)
		{
			timer.isPaused = true;
			Component.Destroy(timer);
		}
		PlayerUI.instance.interactableHoldProgressImage.gameObject.SetActive(false);
		if (GameManager.PlayerHasWireTape || isFixed) PlayerUI.instance.holdInteractionText.gameObject.SetActive(false);
	}

	void SuccessfullyHold()
	{
		print($"{gameObject.name} successfully held");
		StopCoroutine(randomSfxCoroutine);
		PlayerUI.instance.holdInteractionText.text = holdInteractionFinishedText;
		GetComponent<SpatializedAudio>().PlayLoop();
		isFixed = true;
		//TODO : set objective as completed, repair ligths etc
		GameManager.objectiveCount --;
	}

	void Break()
	{
		isFixed = false;
		//TODO : alert player?
	}

	void PlayRandomSFX()
	{
		GetComponent<SpatializedAudio>().PlaySound(2);
	}

	IEnumerator PlayAmbianceAtRandomTime()
	{
		if (randomSfxCoroutine != null) StopCoroutine(randomSfxCoroutine);

		nextRandomSoundTime = Random.Range(minTimeBetweenRandomSounds, maxTimeBetweenRandomSounds);
		yield return new WaitForSeconds(nextRandomSoundTime);

		PlayRandomSFX();
		randomSfxCoroutine = StartCoroutine(PlayAmbianceAtRandomTime());
	}
}
