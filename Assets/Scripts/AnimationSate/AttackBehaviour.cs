using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerHitManager>().IsAttacking = false;
        animator.gameObject.GetComponent<PlayerHitManager>().AttackAnimRunning = false;
    }
}
