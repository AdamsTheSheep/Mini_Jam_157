using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : State
{
	public float RotateSpeed = 5f;
	Quaternion RotateTowards;
	Quaternion startRotation;
	float rotationProgress;
    public override void Enter()
	{
		base.Enter();
		rotationProgress = 0;
		startRotation = enemyReferences.ParentTransform.rotation;
		RotateTowards = Quaternion.Euler(enemyReferences.ParentTransform.rotation.eulerAngles.x, UnityEngine.Random.Range(90,270), enemyReferences.ParentTransform.rotation.eulerAngles.z);
	}

	public override void StateFixedUpdate()
	{
		if (rotationProgress < 1 && rotationProgress >= 0)
		{
       		rotationProgress += Time.deltaTime * RotateSpeed;
        	enemyReferences.ParentTransform.rotation = Quaternion.Lerp(startRotation, RotateTowards, rotationProgress);
    	}
		if (rotationProgress >= 1)
		{
			Transition(this, "FindState");
		}
	}
}
