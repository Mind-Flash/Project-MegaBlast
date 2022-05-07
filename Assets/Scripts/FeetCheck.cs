using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCheck : MonoBehaviour
{
    #region /// Variables
    private GameObject Player;
    private bool frontLegConnecting;
    #endregion

    #region /// Getter/Setters

    public bool FrontLegConnecting 

    {

        get

        {

            return frontLegConnecting;

        }

    }
    #endregion

    // ============================================================

    // Start is called before the first frame update
    void Start()

    {
        Player = transform.parent.gameObject.transform.parent.gameObject;
        frontLegConnecting = true;
    }

    // ============================================================

    #region /// Tells you when front foot is connecting with ground
    private void OnTriggerStay2D(Collider2D collision) 
    {

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "PlayerDead")

        {
            frontLegConnecting = true;

            Player.GetComponent<PlayerControls>().OnGround = true;

        }

        //Debug.Log(FrontLegConnecting);

    }

    #endregion

    // ============================================================

    #region /// Tells you when front foot is NOT connected with ground
    private void OnTriggerExit2D(Collider2D collision)
 
    {

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "PlayerDead")

        {
            frontLegConnecting = false;
        }

        //Debug.Log(FrontLegConnecting);

    }

    #endregion

}
