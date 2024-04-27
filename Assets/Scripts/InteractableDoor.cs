using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour, IInteractable
{
	[SerializeField] GameObject door;
	[SerializeField] Vector3 openPosition;
	[SerializeField] Vector3 closedPosition;
	[SerializeField] float animationDuration;
	[SerializeField] bool isOpen;
	[SerializeField] bool isAnimating;

	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");

		if (isAnimating) return;

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
		float currentAnimationTime = 0f;

		while (currentAnimationTime < animationDuration)
		{
			currentAnimationTime += Time.deltaTime;
			door.transform.localPosition = Vector3.Lerp(closedPosition, openPosition, currentAnimationTime / animationDuration);
			yield return null;
		}

		door.transform.localPosition = openPosition;
		isAnimating = false;
		isOpen = true;
	}

	IEnumerator CloseDoor()
	{
		float currentAnimationTime = 0f;

		while (currentAnimationTime < animationDuration)
		{
			currentAnimationTime += Time.deltaTime;
			door.transform.localPosition = Vector3.Lerp(openPosition, closedPosition, currentAnimationTime / animationDuration);
			yield return null;
		}

		door.transform.localPosition = closedPosition;
		isAnimating = false;
		isOpen = false;
	}
}
