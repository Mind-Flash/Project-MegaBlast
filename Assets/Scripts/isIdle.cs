using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isIdle : StateMachineBehaviour
{

    #region /// Variables

    private GameObject Player;
    private GameObject groundChecker;
    private GameObject ColliderFalling;

    #endregion


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Player = animator.gameObject;
        ColliderFalling = Player.transform.GetChild(1).gameObject;

        #region /// Prevents a bug that makes player be able to stand in air when switching from landing to idle

        if (Player.GetComponent<PlayerControls>().OnGround == false) 

        {

            animator.SetBool("isFalling", true);

            #region /// Switching collider from falling to idle

            Player.GetComponent<BoxCollider2D>().enabled = false;
            ColliderFalling.SetActive(true);

            #endregion

        } 

        else

        {

            Player.GetComponent<EdgeCollider2D>().enabled = true; // turns on edge collider as to not interfere with hit detection

            groundChecker = Player.GetComponentInChildren<GroundDetector>().gameObject;

            Player.GetComponent<Animator>().SetBool("landAnimPlaying", false);

        }

        //==================================================================================

        #endregion


        #region /// Flips groundchecker collider to match new direction
        
        if (Player.GetComponent<SpriteRenderer>().flipX == true && groundChecker.GetComponent<BoxCollider2D>().transform.localPosition.x > 0)


        {

            Vector2 groundCheckerPosition = new Vector2((groundChecker.GetComponent<BoxCollider2D>().transform.localPosition.x) * (-1), groundChecker.GetComponent<BoxCollider2D>().transform.localPosition.y);

            groundChecker.GetComponent<BoxCollider2D>().transform.localPosition = groundCheckerPosition;

        }
        
        else if (Player.GetComponent<SpriteRenderer>().flipX == false && groundChecker.GetComponent<BoxCollider2D>().transform.localPosition.x < 0)


            {

            Vector2 groundCheckerPosition = new Vector2((groundChecker.GetComponent<BoxCollider2D>().transform.localPosition.x) * (-1), groundChecker.GetComponent<BoxCollider2D>().transform.localPosition.y);

            groundChecker.GetComponent<BoxCollider2D>().transform.localPosition = groundCheckerPosition;

            }

        //==================================================================================

        #endregion

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool("isIdle", false);

    }





    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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
