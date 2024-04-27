using UnityEngine;

public class InteractableObjectiveBoxGenerator : MonoBehaviour, IInteractable
{
	public void Interact()
	{
		if(!GameManager.playerHasFixedGenerator)
			PlayerUI.instance.StartGeneratorMinigame();
	}

	public void StopInteract()
	{
	}
}
