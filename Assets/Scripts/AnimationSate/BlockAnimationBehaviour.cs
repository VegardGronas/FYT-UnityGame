using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimationBehaviour : StateMachineBehaviour
{
    public GameObject blockVFX;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("Dead"))
        {
            animator.GetComponent<PlayerMovement>().isBlocking = true;
        }
        else
        {
            if (blockVFX != null)
            {
                animator.GetComponent<PlayerAttack>().InstantiaeBlockVFX(blockVFX, 1f);
                blockVFX.GetComponent<AudioSource>().volume = SoundManager.Instance.FXVolumeSource.volume;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerMovement>().isBlocking = false;
    }
}
