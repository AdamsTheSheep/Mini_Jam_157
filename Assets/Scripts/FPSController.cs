using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
	[SerializeField] Camera playerCamera;
	[SerializeField] float walkSpeed = 6f;
	[SerializeField] float runSpeed = 12f;
	[SerializeField] float jumpPower = 7f;
	[SerializeField] float gravity = 10f;
	[SerializeField] float lookSpeed = 2f;
	[SerializeField] float lookXLimit = 45f;

	public bool canMove = true;

	new Transform transform;
	public Vector3 moveDirection = Vector3.zero;
	float rotationX = 0;
	CharacterController characterController;

	private void Start()
	{
		transform = GetComponent<Transform>();
		characterController = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		if (PlayerUI.instance.usingGenerator) return;

		//MOVEMENT
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		Vector3 right = transform.TransformDirection(Vector3.right);

		bool isRunning = Input.GetKey(KeyCode.LeftShift);
		float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
		float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
		float movementDirectionY = moveDirection.y;
		moveDirection = (forward * curSpeedX) + (right * curSpeedY);

		//JUMPING
		if (Input.GetButton("Jump") && canMove && characterController.isGrounded) moveDirection.y = jumpPower;
		else moveDirection.y = movementDirectionY;

		if (!characterController.isGrounded) moveDirection.y -= gravity * Time.deltaTime;

		//ROTATION
		characterController.Move(moveDirection * Time.deltaTime);

		if (canMove)
		{
			rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
			rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
			playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
			transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
		}
	}
}
