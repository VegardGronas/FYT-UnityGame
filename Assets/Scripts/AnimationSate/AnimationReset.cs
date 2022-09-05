using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReset : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("Dead"))
        {
            animator.SetBool("CheckDeath", false);
        }
        else
        {
            animator.SetInteger("Attacks", 0);
            animator.SetBool("IdleState", true);
            animator.GetComponent<PlayerMovement>().isHit = false;
            animator.gameObject.GetComponent<PlayerHitManager>().IsAttacking = false;
            animator.gameObject.GetComponent<PlayerHitManager>().CanAttack = false;
            animator.gameObject.GetComponent<PlayerHitManager>().AttackAnimRunning = false;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IdleState", false);
    }
}
