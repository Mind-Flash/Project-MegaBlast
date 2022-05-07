using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasLanded : StateMachineBehaviour
{
    #region /// variables
    private GameObject Player;
    private GameObject ColliderFalling;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Player = animator.gameObject;

        // ===============================================
        // Below: switching colliders
        // ===============================================

        ColliderFalling = Player.transform.GetChild(1).gameObject;
        ColliderFalling.SetActive(false);
        Player.GetComponent<BoxCollider2D>().enabled = true; //Switching Collider

        // ===============================================

        animator.SetBool("isRunning", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("landAnimPlaying", true);
        animator.SetBool("wallCollision", false);

        Player.transform.GetChild(1).gameObject.GetComponent<WallDetector>().WallTouchLeft = false;
        Player.transform.GetChild(1).gameObject.GetComponent<WallDetector>().WallTouchRight = false;


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("hasLanded", true);
    }

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

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
