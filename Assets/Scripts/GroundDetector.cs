using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    #region /// Variables

    GameObject Player;
    private GameObject colliderFalling;
    private GameObject feetCollider;

    bool backLegConnecting;

    #endregion

    #region /// Getter/Setters

    public GameObject FeetCollider

    {
       
        get

        {

            return feetCollider;

        }


    }

    #endregion
    // ============================================================

    void Start()
    
    {
        Player = gameObject.transform.parent.gameObject;
        colliderFalling = Player.transform.GetChild(1).gameObject;
        feetCollider = gameObject.transform.GetChild(0).gameObject;
    }
    // ============================================================

    /// Note: needed to fix a bug that otherwise triggers the wrong colliders to spawn when running into one wall and then another
    private void OnTriggerStay2D(Collider2D collision) 

    {

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "PlayerDead")

        {

            Player.GetComponent<BoxCollider2D>().enabled = true; //Switching to idle Collider
            colliderFalling.SetActive(false);

            backLegConnecting = true;
        }

        if (transform.GetChild(0).gameObject.GetComponent<FeetCheck>().FrontLegConnecting == false && backLegConnecting == true) // checking if other leg is touching ground

        {
            Debug.Log("ONE LEG STAND");
        }

    }
    // ============================================================

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "PlayerDead")

        {

            backLegConnecting = true;

            Player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = true; // Below: Activating collider for front foot              


            Player.GetComponent<PlayerControls>().OnGround = true;
            Player.GetComponent<PlayerControls>().AirPush = false;

            Player.GetComponent<Animator>().SetBool("wallCollision", false);

            colliderFalling.SetActive(false);
            Player.GetComponent<BoxCollider2D>().enabled = true; //Switching to idle Collider

            Debug.Log("I just hit ground"); // Debug Only!

        }

    }
    // ============================================================

    private void OnTriggerExit2D(Collider2D collision)
    {
        //===============================================
        //Below: Switch collider as soon as leaving ground
        //===============================================

        //===============================================
        Player.GetComponent<BoxCollider2D>().enabled = false;
        colliderFalling.SetActive(true); // Switch to falling collider
        //===============================================

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "PlayerDead")


        {

            backLegConnecting = false;

            gameObject.GetComponentInParent<PlayerControls>().OnGround = false;

            gameObject.GetComponentInParent<PlayerControls>().AirPush = true;



            if (Player.GetComponent<Animator>().GetBool("isIdle") == true)

            {
                Player.GetComponent<Animator>().SetBool("isFalling", true); // makes sure you can't go into "idle" animation state if in air
            }

        }

    }

}
