using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDebugLogger : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log($"Interacted with {gameObject.name}");
    }

    public void StopInteract()
    {
        Debug.Log($"Stopped interacting with {gameObject.name}");
    }
}
