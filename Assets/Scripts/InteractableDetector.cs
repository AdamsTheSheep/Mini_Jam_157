using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
	[SerializeField] float range = 3f;

	new Transform transform;
	bool isInteracting = false;
	IInteractable lastInsteracted;

	private void Awake()
	{
		transform = GetComponent<Transform>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
		{
			Ray r = new Ray(transform.position, transform.forward);
			if(Physics.Raycast(r, out RaycastHit hitInfo, range))
			{
				if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
				{
					lastInsteracted = interactable;
					interactable.Interact();
					isInteracting = true;
				}
			}
		}

		if(isInteracting && lastInsteracted!=null && Input.GetKeyUp(KeyCode.E))
		{
			lastInsteracted.StopInteract();
			isInteracting = false;
			lastInsteracted = null;
		}
	}
}
