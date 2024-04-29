using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyReferences : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
	public Transform ParentTransform;
	public Transform Foot;
	public Animator animator;
	public StateMachine stateMachine;
}
