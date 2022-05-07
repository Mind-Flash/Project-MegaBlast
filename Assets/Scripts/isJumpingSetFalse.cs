using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isJumpingSetFalse : StateMachineBehaviour
{
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

            
            animator.SetBool("isJumpMoving", false);

            animator.SetBool("isJumping", false);

            animator.SetBool("isFalling", false);

            animator.SetBool("isLanding", false);

            animator.SetBool("isRunning", false);

            animator.SetBool("isJumpMoving", false);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.GetBool("isJumpMoving") == true)

        {

            animator.SetBool("isJumpMoving", false); // Need this to correct a bug where you could be in an idle animation state when jumping

        }
     
    }





    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
