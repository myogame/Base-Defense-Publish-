using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackAnimation : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.gameObject.GetComponent<Boss>())
            animator.gameObject.GetComponent<Boss>().attacking = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.gameObject.GetComponent<Boss>())
            animator.gameObject.GetComponent<Boss>().attacking = false;

    }


}
