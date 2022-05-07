using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isRunning : StateMachineBehaviour
{

    #region /// Variables

    private GameObject Player;

    #endregion


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Player = animator.gameObject;

        #region /// Switching collider from falling to idle

        Player.GetComponent<EdgeCollider2D>().enabled = false;  // turns of edge collider as to not interfere with hit detection
        Player.GetComponent<BoxCollider2D>().enabled = true; 

        #endregion

        animator.SetBool("isFalling", false);
        animator.SetBool("isLanding", false);
        animator.SetBool("isJumpMoving", false);
        animator.SetBool("landAnimPlaying", false);
        animator.SetBool("isJumping", false);

        Player.GetComponent<EdgeCollider2D>().enabled = false;  // turns of edge collider as to not interfere with hit detection

    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
