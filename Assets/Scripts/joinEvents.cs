using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; // Needed to find the PlayerInput component
using UnityEngine;

public class joinEvents : MonoBehaviour
{

    private GameObject playerPrefab;
    private Vector3 flipPlayer;

    private void OnPlayerJoined() //Player join event (Coupled with Player Controller)

    {


        #region ///Join Event for player 1
        if (GameObject.Find("P1") == false)

        {

            playerPrefab = GameObject.Find("PlayerDefault(Clone)");

            playerPrefab.name = "P1";

            playerPrefab.GetComponent<SpriteRenderer>().enabled = true;

            playerPrefab.GetComponent<BoxCollider2D>().isTrigger = false;

            playerPrefab.GetComponent<PlayerControls>().enabled = true;

            playerPrefab.AddComponent<P1Health>();

        }
        #endregion

        #region /// Join Event for player 2
        if (GameObject.Find("P1") == true)

        {

            playerPrefab = GameObject.Find("PlayerDefault(Clone)");

            flipPlayer = new Vector3(playerPrefab.transform.localScale.x * -1, playerPrefab.transform.localScale.y, playerPrefab.transform.localScale.z);

            playerPrefab.name = "P2";

            playerPrefab.GetComponent<Transform>().position = new Vector2(9.28f, -5.985002f);

            playerPrefab.GetComponent<Transform>().localScale = flipPlayer; // flip character the correct way

            //playerPrefab.GetComponent<Transform>().localScale = new Vector3(playerPrefab.transform.localScale.x * -1, playerPrefab.transform.localScale.y, playerPrefab.transform.localScale.z); // flip character the correct way

            playerPrefab.GetComponent<BoxCollider2D>().isTrigger = false;

            //playerPrefab.GetComponent<SpriteRenderer>().flipX = true; //Depracated not working fully with CollisionBoes better to scale * -1

            playerPrefab.GetComponent<SpriteRenderer>().enabled = true;

            playerPrefab.GetComponent<PlayerControls>().enabled = true;  

            playerPrefab.AddComponent<P2Health>();

        }
        #endregion

    }

}
