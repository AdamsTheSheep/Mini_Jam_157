using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
	[SerializeField] GameObject door;
	[SerializeField] Vector3 openPosition;
	[SerializeField] Vector3 closedPosition;
	[SerializeField] float animationDuration;
	[SerializeField] bool startOpen;
	public bool canMonsterInteract = true;

	bool isAnimating;
	Vector3 InitialPos;
	public bool isOpen;

	private void Start()
	{
		InitialPos = transform.localPosition;
		openPosition = InitialPos + openPosition;
		closedPosition = InitialPos + closedPosition;
		if(startOpen) door.transform.localPosition = openPosition;
		isOpen = true;
	}

	[ContextMenu("OpenDoor")]
	public void Open()
	{
		if (!isOpen && !isAnimating)
		StartCoroutine(OpenDoor());
	}

	[ContextMenu("CloseDoor")]
	public void Close()
	{
		if (isOpen && !isAnimating)
		StartCoroutine (CloseDoor());
	}

	public IEnumerator OpenDoor()
	{
		GetComponent<SpatializedAudio>().PlaySound();
		float currentAnimationTime = 0f;
		isAnimating = true;
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

	public IEnumerator CloseDoor()
	{
		GetComponent<SpatializedAudio>().PlaySound(2);
		float currentAnimationTime = 0f;
		isAnimating = true;
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
