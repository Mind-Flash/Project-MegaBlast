using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabLedge : MonoBehaviour
{
    [SerializeField]
    PlayerControls myPlayer;
    [SerializeField]
    Input_Controls inputControls;
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {
            EdgeHandler edge = collision.gameObject.GetComponent<EdgeHandler>();
            myPlayer.SetGrabable(edge);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {
            myPlayer.SetGrabable(null);
        }
    }
}
