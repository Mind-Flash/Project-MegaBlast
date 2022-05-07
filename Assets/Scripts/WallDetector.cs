using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{

    #region ///Variables
    private GameObject Player;

    public bool wallTouchLeft; //!!!Note: Only public for debugging otherwise private!!!
    public bool wallTouchRight; //!!!Note: Only public for debugging otherwise private!!!

    #endregion


    #region /// Getter/Setters
    public bool WallTouchLeft

    {

        set

        {


            wallTouchLeft = value;


        }

        get

        {


            return wallTouchLeft;

        }


    }

    // ============================================================

    public bool WallTouchRight

    {
        set

        {


            wallTouchRight = value;


        }

        get

        {


            return wallTouchRight;

        }


    }

    // ============================================================

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;

        wallTouchLeft = false;
        wallTouchRight = false;
    }

    #region /// Methods/Functions

    #region /// Checking for collisions with walls when jumping
    private void CheckWallCollisionSide(Collision2D collisionObj) 

    {

        if (collisionObj.otherCollider is BoxCollider2D && Player.GetComponent<PlayerControls>().FacingRight == true) // facing right and the right collider is connecting to a wall, indicate that right side is touching a wall

        {


            wallTouchRight = true;

            //Debug.Log("Touching Right");


            Player.GetComponent<Animator>().SetBool("isFalling", true); // if jumping into wall, change into fall animation



        }

        // Below: facing left need this as the right collider now is the left one when facing left
        else if (collisionObj.otherCollider is BoxCollider2D && Player.GetComponent<PlayerControls>().FacingRight == false) 

        {


            wallTouchLeft = true;

            //Debug.Log("Touching Left");

            Player.GetComponent<Animator>().SetBool("isFalling", true); // if jumping into wall, change into fall animation

        }

        //========================================================================================================

        //========================================================================================================


        // Below: facing left need this as the right collider now is the left one when facing left
        if (collisionObj.otherCollider is EdgeCollider2D && Player.GetComponent<PlayerControls>().FacingRight == true) 


        {


            wallTouchLeft = true;

            //Debug.Log("Touching Left");

            Player.GetComponent<Animator>().SetBool("isFalling", true); // if jumping into wall, change into fall animation


        }

        else if (collisionObj.otherCollider is EdgeCollider2D && Player.GetComponent<PlayerControls>().FacingRight == false) // facing left need this as the right collider now is the left one when facing left

        {


            wallTouchRight = true;

            //Debug.Log("Touching Right");

            Player.GetComponent<Animator>().SetBool("isFalling", true); // if jumping into wall, change into fall animation

        }

        //========================================================================================================

    }
    //=====================================================================================================
    #endregion

    #region /// depracated function delete if not affecting anything
    /*
    private void WallSideCollissionCheck (Collision2D collision) 

    {

        CheckWallCollisionSide(collision);

    }
    //=====================================================================================================
    */

    #endregion

    #endregion

    //Note: Need this check as well as onStay, as this little frame breaks the flow if colliding 

    private void OnCollisionEnter2D(Collision2D collision) 

    {

        CheckWallCollisionSide(collision);

    }

    // ============================================================

    private void OnCollisionStay2D(Collision2D collision) // tells which side is colliding with wall

    {


        if (collision.gameObject.tag != "Player")

        {
            Player.GetComponent<Animator>().SetBool("wallCollision", true);
            Player.GetComponent<Animator>().SetBool("isJumpMoving", false);


            // Below: Telling which side is connected to a wall when jumping

            //========================================================================================================

            if (collision.otherCollider is BoxCollider2D && Player.GetComponent<PlayerControls>().FacingRight == true) // facing right and the right collider is connecting to a wall, indicate that right side is touching a wall

            {
                

                wallTouchRight = true;


                Player.GetComponent<Animator>().SetBool("isFalling", true); // if jumping into wall, change into fall animation

               

            }

            else if (collision.otherCollider is BoxCollider2D && Player.GetComponent<PlayerControls>().FacingRight == false) // facing left need this as the right collider now is the left one when facing left

            {


                wallTouchLeft = true;

                Player.GetComponent<Animator>().SetBool("isFalling", true); // if jumping into wall, change into fall animation

            }

            //========================================================================================================


            //========================================================================================================

            if (collision.otherCollider is EdgeCollider2D && Player.GetComponent<PlayerControls>().FacingRight == true) // facing left need this as the right collider now is the left one when facing left


            {


                wallTouchLeft = true;

                Player.GetComponent<Animator>().SetBool("isFalling", true); // if jumping into wall, change into fall animation


            }

            else if (collision.otherCollider is EdgeCollider2D && Player.GetComponent<PlayerControls>().FacingRight == false) // facing left need this as the right collider now is the left one when facing left

            {


                wallTouchRight = true;

                Player.GetComponent<Animator>().SetBool("isFalling", true); // if jumping into wall, change into fall animation

            }

            //========================================================================================================

        }

    }
    //=====================================================================================================

    //=====================================================================================================
    private void OnCollisionExit2D(Collision2D collision) // reset walltouch values when leaving wall

    {


        if (collision.gameObject.tag != "Player")

        {

            Player.GetComponent<Animator>().SetBool("wallCollision", false);

        }


        // Below: Removing states when correct collider moves away from wall.

        if (collision.otherCollider is BoxCollider2D && Player.GetComponent<PlayerControls>().FacingRight == true) // facing right and the right collider is connecting to a wall, indicate that right side is touching a wall

        {


            wallTouchRight = false;




        }

        else if (collision.otherCollider is BoxCollider2D && Player.GetComponent<PlayerControls>().FacingRight == false) // facing left need this as the right collider now is the left one when facing left

        {


            wallTouchLeft = false;


        }

        //========================================================================================================



        //========================================================================================================

        if (collision.otherCollider is EdgeCollider2D && Player.GetComponent<PlayerControls>().FacingRight == true) // facing left need this as the right collider now is the left one when facing left


        {


            wallTouchLeft = false;



        }

        else if (collision.otherCollider is EdgeCollider2D && Player.GetComponent<PlayerControls>().FacingRight == false) // facing left need this as the right collider now is the left one when facing left

        {



            wallTouchRight = false;


        }

        //========================================================================================================
    }
    //=====================================================================================================

}


