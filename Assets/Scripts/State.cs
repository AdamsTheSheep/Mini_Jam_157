using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
	[HideInInspector] public EnemyReferences enemyReferences;
	public GameManager.StateEvent Transition;
    public virtual void Enter(){}
    public virtual void StateUpdate(){}
	public virtual void StateFixedUpdate(){}
	public virtual void Exit(){}
}
