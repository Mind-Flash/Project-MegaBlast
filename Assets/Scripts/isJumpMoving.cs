using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isJumpMoving : StateMachineBehaviour
{

    #region /// Variables

    private GameObject Player;
    private GameObject ColliderFalling;
    private GameObject master;

    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)

    {

        Player = animator.gameObject;
        ColliderFalling = Player.transform.GetChild(1).gameObject;
        master = GameObject.Find("Master");


        #region /// switching colliders from idle to falling

        Player.GetComponent<BoxCollider2D>().enabled = false; 
        ColliderFalling.SetActive(true); 
        // ===============================================

        #endregion

        Player.GetComponent<EdgeCollider2D>().enabled = false; // turns of edge collider as to fix clipping error when landing (interferes with boxcollider somehow)

        animator.SetBool("hasLanded", false);
        animator.SetBool("isFalling", false);
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", false);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)

    {

            if (Player.GetComponent<PlayerControls>().OnGround == true && Player.GetComponent<Rigidbody2D>().velocity.y <= 0) // if player lands set animation state to "Landing"

            {

                animator.SetBool("isLanding", true);
                animator.SetBool("landAnimPlaying", true);

            #region /// Instantiates FX "LandingSmoke"

            Vector2 landSmokePosRight; //position to spawn right landing smoke by feet
            Vector2 landSmokePosLeft; //position to spawn left landing smoke by feet
            landSmokePosRight = new Vector2(Player.GetComponent<BoxCollider2D>().transform.localPosition.x + 1.8f, Player.transform.position.y - 1.35f);
            landSmokePosLeft = new Vector2(Player.GetComponent<BoxCollider2D>().transform.localPosition.x + (1.8f * -1), Player.transform.position.y - 1.35f);

            GameObject LandingSmokeInstantiatedRight = Instantiate(Resources.Load("Prefabs/LandingSmoke_Right") as GameObject, landSmokePosRight, Player.transform.rotation); // Spawns FX at player feet (Right)
            GameObject LandingSmokeInstantiatedLeft = Instantiate(Resources.Load("Prefabs/LandingSmoke_Left") as GameObject, landSmokePosLeft, Player.transform.rotation); // Spawns FX at player (Left)

            #endregion

        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool("isJumping", false);

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
}
