using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isBlastingSetFalse : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Below: applying correct animations for shooting 

        if (animator.GetBool("isBlasting_001") == true)

        { 

        animator.SetBool("isBlasting_001", false); // makes sure idle is played so blast anim is not looping when not firing

        }

        if (animator.GetBool("isBlasting_002") == true)

        {

            animator.SetBool("isBlasting_002", false); // makes sure idle is played so blast anim is not looping when not firing

        }

        if (animator.GetBool("isBlasting_003") == true)

        {

            animator.SetBool("isBlasting_003", false); // makes sure idle is played so blast anim is not looping when not firing

        }

        if (animator.GetBool("isJumpBlasting_001") == true)

        {

            animator.SetBool("isJumpBlasting_001", false); // makes sure idle is played so blast anim is not looping when not firing

        }

        if (animator.GetBool("isJumpBlasting_002") == true)

        {

            animator.SetBool("isJumpBlasting_002", false); // makes sure idle is played so blast anim is not looping when not firing

        }

        if (animator.GetBool("isJumpBlasting_003") == true)

        {

            animator.SetBool("isJumpBlasting_003", false); // makes sure idle is played so blast anim is not looping when not firing

        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
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
