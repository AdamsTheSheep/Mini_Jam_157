using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
	[SerializeField] float range = 3f;

	new Transform transform;
	bool isInteracting = false;
	bool facingInteractable = false;
	IInteractable lastInsteracted;

	bool FacingInteractable
	{
		get { return facingInteractable; }
		set
		{
			if (facingInteractable != value)
			{
				facingInteractable = value;
				PlayerUI.instance.pointerImage.color = value ? Color.red : Color.white;
			}
		}
	}

	private void Awake()
	{
		transform = GetComponent<Transform>();
	}

	private void Update()
	{
		Ray r = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(r, out RaycastHit hitInfo, range))
		{
			if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
			{
				FacingInteractable = true;

				if (!isInteracting && Input.GetKeyDown(KeyCode.E))
				{
					lastInsteracted = interactable;
					interactable.Interact();
					isInteracting = true;
				}
			}
			else
			{
				FacingInteractable = false;
			}
		}
		else
		{
			FacingInteractable = false;
		}

		if (isInteracting && lastInsteracted != null && Input.GetKeyUp(KeyCode.E))
		{
			lastInsteracted.StopInteract();
			isInteracting = false;
			lastInsteracted = null;
		}
	}
}
