using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAnimation : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.GetComponent<EnemyControler>())
            animator.gameObject.GetComponent<EnemyControler>().attacking = true;


    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.GetComponent<EnemyControler>())
            animator.gameObject.GetComponent<EnemyControler>().attacking = false;


    }


}
