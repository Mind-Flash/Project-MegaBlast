using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isFallingTrue : StateMachineBehaviour
{

    #region /// Variables
    private GameObject Player;
    private GameObject colliderFalling;
    private GameObject master;

    BoxCollider2D frontFoot;
    #endregion


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isJumpMoving", false);
        animator.SetBool("hasLanded", false);


        Player = animator.gameObject;
        colliderFalling = Player.transform.GetChild(1).gameObject;
        master = GameObject.Find("Master");
     
        frontFoot = Player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();

        frontFoot.enabled = false; //Enable Front foot collision detection


        #region /// Switching colliders from idle collider to falling

        Player.GetComponent<BoxCollider2D>().enabled = false; 
        colliderFalling.SetActive(true); 

        // ===============================================
        #endregion


        Player.GetComponent<EdgeCollider2D>().enabled = false; // turns of edge collider (connection to floor) as to not interfere with hit detection

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (master.GetComponent<MainSetup>().IsFalling(Player) == true) // if player is falling trigger fall animation state

        {

            animator.SetBool("isFalling", true);

        }

    }













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
