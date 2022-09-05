using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBehaviour : StateMachineBehaviour
{


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.gameObject.GetComponent<PlayerMovement>().isHit = true;
        animator.SetInteger("Reactions", 0);
        animator.SetInteger("GeneralReactions", 0);
    }
}
