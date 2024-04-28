using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : State
{
	public float RotateSpeed = 0.1f;
	Vector3 RotateTowards;
    public override void Enter()
	{
		base.Enter();
		RotateTowards = Quaternion.AngleAxis(UnityEngine.Random.Range(90, 180), Vector3.right) * enemyReferences.ParentTransform.forward;
	}

	public override void StateFixedUpdate()
	{
		base.StateFixedUpdate();
		Vector3 relativePos = RotateTowards - enemyReferences.ParentTransform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
		enemyReferences.ParentTransform.rotation = Quaternion.Lerp(enemyReferences.ParentTransform.rotation, rotation, Time.time * RotateSpeed);
		if (Math.Abs(enemyReferences.ParentTransform.rotation.eulerAngles.y - rotation.eulerAngles.y) < 1)
		{
			Transition(this, "FindState");
		}
	}
}
