public class InteractableObjectiveBoxGenerator : IInteractable
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
