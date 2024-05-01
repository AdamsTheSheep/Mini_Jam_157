using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoubleDoor : MonoBehaviour, IInteractable
{
	[SerializeField] GameObject leftDoor;
	[SerializeField] GameObject rightDoor;
	[SerializeField] Vector3 leftOpenPosition;
	[SerializeField] Vector3 rightOpenPosition;
	[SerializeField] Vector3 leftClosedPosition;
	[SerializeField] Vector3 rightClosedPosition;
	[SerializeField] float animationDuration;
	[SerializeField] bool isOpen;
	[SerializeField] bool isAnimating;

	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");

		if (GameManager.objectiveCount > 0 || isAnimating) return;

		isAnimating = true;
		if (isOpen)
		{
			StartCoroutine(CloseDoor());
		}
		else
		{
			StartCoroutine(OpenDoor());
		}
	}

	public void StopInteract() { }

	IEnumerator OpenDoor()
	{
		GetComponent<SpatializedAudio>().PlaySound();
		float currentAnimationTime = 0f;

		while (currentAnimationTime < animationDuration)
		{
			currentAnimationTime += Time.deltaTime;
			leftDoor.transform.localPosition = Vector3.Lerp(leftClosedPosition, leftOpenPosition, currentAnimationTime / animationDuration);
			rightDoor.transform.localPosition = Vector3.Lerp(rightClosedPosition, rightOpenPosition, currentAnimationTime / animationDuration);
			yield return null;
		}

		leftDoor.transform.localPosition = leftOpenPosition;
		rightDoor.transform.localPosition = rightOpenPosition;
		isAnimating = false;
		isOpen = true;
	}

	IEnumerator CloseDoor()
	{
		GetComponent<SpatializedAudio>().PlaySound(2);
		float currentAnimationTime = 0f;

		while (currentAnimationTime < animationDuration)
		{
			currentAnimationTime += Time.deltaTime;
			leftDoor.transform.localPosition = Vector3.Lerp(leftOpenPosition, leftClosedPosition, currentAnimationTime / animationDuration);
			rightDoor.transform.localPosition = Vector3.Lerp(rightOpenPosition, rightClosedPosition, currentAnimationTime / animationDuration);
			yield return null;
		}

		leftDoor.transform.localPosition = leftClosedPosition;
		rightDoor.transform.localPosition = rightClosedPosition;
		isAnimating = false;
		isOpen = false;
	}
}
