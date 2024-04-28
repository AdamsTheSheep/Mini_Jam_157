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
	[SerializeField] bool isOpen;

	private void Start()
	{
		if(startOpen) door.transform.localPosition = openPosition;
		isOpen = true;
	}

	[ContextMenu("OpenDoor")]
	public void Open()
	{
		StartCoroutine(OpenDoor());
	}

	[ContextMenu("CloseDoor")]
	public void Close()
	{
		StartCoroutine (CloseDoor());
	}

	public IEnumerator OpenDoor()
	{
		float currentAnimationTime = 0f;

		while (currentAnimationTime < animationDuration)
		{
			currentAnimationTime += Time.deltaTime;
			door.transform.localPosition = Vector3.Lerp(closedPosition, openPosition, currentAnimationTime / animationDuration);
			yield return null;
		}

		door.transform.localPosition = openPosition;
		isOpen = true;
	}

	public IEnumerator CloseDoor()
	{
		float currentAnimationTime = 0f;

		while (currentAnimationTime < animationDuration)
		{
			currentAnimationTime += Time.deltaTime;
			door.transform.localPosition = Vector3.Lerp(openPosition, closedPosition, currentAnimationTime / animationDuration);
			yield return null;
		}

		door.transform.localPosition = closedPosition;
		isOpen = false;
	}
}
