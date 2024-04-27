using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractableGrab : MonoBehaviour, IInteractable
{
	[SerializeField] float PickUpForce;
	[SerializeField] float Distance;
	bool isHolding = false;
	Rigidbody body;

	float drag;
	bool useGravity;
	RigidbodyConstraints constraints;

	public void Start()
	{
		gameObject.layer = LayerMask.NameToLayer("Grabbable");
		body = gameObject.GetComponent<Rigidbody>();
		if (body == null)
		{
			Debug.Log("gameObject " + gameObject.name + "'s Rigibody not found.");
		}
		else
		{
			drag = body.drag;
			useGravity = body.useGravity;
			constraints = body.constraints;
		}
	}
	public void Interact()
	{
		Debug.Log($"Interacted with {gameObject.name}");
		isHolding = true;
		if (body != null)
		{
			body.drag = 10f;
			body.useGravity = false;
			body.constraints = RigidbodyConstraints.FreezeRotation;
			Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player"),true);
		}
	}

	public void StopInteract()
	{
		Debug.Log($"Stopped interacting with {gameObject.name}");
		isHolding = false;
		if (body != null)
		{
			body.drag = drag;
			body.useGravity = useGravity;
			body.constraints = constraints;
			Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Player"),false);
		}
	}
   private void Update()
	{
		if (isHolding)
		{
			if (GameObject.FindGameObjectWithTag("Player"))
			{
				var p = GameObject.FindGameObjectWithTag("Player");
				var target = p.transform.position + Camera.main.transform.forward * Distance;
				move(p, target);
			}
			else {Debug.Log(transform.name + "attempt to find GameObject with tag \"Player\" failed.");}
		}
	}

	private void move(GameObject p, Vector3 target)
	{
		if (Vector3.Distance(transform.position, target) > 0.1f)
		{
			target = target - transform.position;
			body.AddForce(target * PickUpForce);
		}
	}
}
