using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimController : MonoBehaviour
{
	public enum States {Idle1, Idle2, Move, Attack}
	public EnemyReferences references;
	public static bool isAttacking;
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
				references.animator.speed = 1.5f;
				references.animator.SetInteger("CurrentState", ((int)States.Move));
				break;
			case Chase:
			Debug.Log(isAttacking);
				if (EntityAnimController.isAttacking) break;
				references.animator.speed = 2f;
				references.animator.SetInteger("CurrentState", ((int)States.Move));
				break;
		}
    }
}
