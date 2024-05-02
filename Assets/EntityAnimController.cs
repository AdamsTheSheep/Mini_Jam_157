using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimController : MonoBehaviour
{
	public enum States {Idle1, Idle2, Move, Attack}
	public EnemyReferences references;
	public static bool isAttacking = false;

	void Start()
	{
		isAttacking = false;
	}
    void Update()
    {
        switch (references.stateMachine.CurrentState)
		{
			case FindState:
				references.animator.speed = 0.25f;
				if (Random.Range(0,2) == 0) references.animator.SetInteger("CurrentState",((int)States.Idle1));
				else references.animator.SetInteger("CurrentState",((int)States.Idle2));
				break;
			case Roaming:
				references.animator.speed = 0.75f;
				references.animator.SetInteger("CurrentState", ((int)States.Move));
				break;
			case Suspicious:
				references.animator.speed = 1.25f;
				references.animator.SetInteger("CurrentState", ((int)States.Move));
				break;
			case Chase:
				if (EntityAnimController.isAttacking) break;
				references.animator.speed = 2f;
				references.animator.SetInteger("CurrentState", ((int)EntityAnimController.States.Move));
				break;
		}
		if (references.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !references.animator.IsInTransition(0) && isAttacking == true) isAttacking = false;
    }
}
