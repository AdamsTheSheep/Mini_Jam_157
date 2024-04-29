using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpatializedAudio))]
public class InteractableObjectiveBoxHold : MonoBehaviour, IInteractable
{
	[SerializeField] float holdTime = 5f;
	[SerializeField] string holdInteractionText;
	[SerializeField] string holdInteractionFinishedText;
	[SerializeField] bool Finished = false;
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
		if (Finished) return;
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

		CancelInvoke();
		timer.isPaused = true;
		Component.Destroy(timer);
		PlayerUI.instance.interactableHoldProgressImage.gameObject.SetActive(false);
		PlayerUI.instance.holdInteractionText.gameObject.SetActive(false);
	}

	void SuccessfullyHold()
	{
		print($"{gameObject.name} successfully held");
		Finished = true;
		GetComponent<SpatializedAudio>().PlaySound();
		StopCoroutine(randomSfxCoroutine);
		GameManager.objectiveCount --;
		PlayerUI.CheckAllFixed();
		//TODO : set objective as completed, repair ligths etc
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
