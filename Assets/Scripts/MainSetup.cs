using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Note! Need this to load scenes/restart game
using UnityEngine.UI; // Note! Need this to edit text objects 
using UnityEngine.InputSystem; // Behövs för att kunna använda Unitys senaste input system för kontroller

public class MainSetup : MonoBehaviour


{

    #region /// Variables

    private int randBlast; // Set random blast anim, 1 corresponds to blasting_001 and so on...

	public Text winnerText; // Note drag/apply object manually from scene!

	public GameObject menuPaus; // Note drag/apply object manually from scene!
	public GameObject menuScore; // Note drag/apply object manually from scene!

	private GameObject player1; 
	private GameObject player2; 

	private GameObject p1GuiText;

	private bool gamePaused;

	private UnityEngine.InputSystem.Controls.KeyControl pausKey; // press this key to restart game from paus menu  
	private UnityEngine.InputSystem.Controls.KeyControl restartKey; // press this key to restart game from scoreboard menu

    #endregion

    #region /// Getter/Setters
    public int RandBlast

	{


		get

		{

			return randBlast;

		}

		set


		{

			randBlast = value;

		}


	}

    #endregion

    void Start()

	{
		randBlast = Random.Range(1, 4);

		player1 = GameObject.Find("P1");
		player2 = GameObject.Find("P2");

		p1GuiText = GameObject.Find("Text_P1Health");


		gamePaused = false;
		pausKey = Keyboard.current.escapeKey;
		restartKey = Keyboard.current.enterKey;

	}

	#region /// Functions/Methods

	
	public void PausGame() 



	{

		if (menuScore.activeSelf == false) // Makes sure you cannot pause game when scoreboard is displaying

		{


			if (menuPaus.activeSelf == true) // Paus game

			{

				gamePaused = false;

				menuPaus.SetActive(false);

				Time.timeScale = 1; // Plays all time based operations such as "tick" and physics in real time

			}


			else if (menuPaus.activeSelf == false) // Unpaus game

			{

				gamePaused = true;

				menuPaus.SetActive(true);

				Time.timeScale = 0; // Pauses all time based operations such as "tick" and physics

			}

		}

	}
	//===========================

	public void ActivateScoreScreen()

	{

		gamePaused = true;

		Time.timeScale = 0; // Pauses all time based operations such as "tick" and physics

		menuScore.SetActive(true);

	}
	//===========================

	public void SetWinnerText()

	{


		if (p1GuiText.GetComponent<HealthUpdateP1>().Lives == 0)

		{

			winnerText.text = "Player: 2 Wins!";

		}

		else

		{

			winnerText.text = "Player: 1 Wins!";

		}

	}
	//===========================

	public void RestartGame()

	{

		if (restartKey.isPressed)

		{

			gamePaused = false;

			Time.timeScale = 1; // Pauses all time based operations such as "tick" and physics

			SceneManager.LoadScene("Scene_Test");

		}
	}
    //===========================
    
    public bool IsFalling(GameObject Player)


	{


		if (Player.GetComponent<Rigidbody2D>().velocity.y < 0)

			return true;

		else return false;

	}
    //===========================
    #endregion


    #region /// Getter/Setters
    public bool GamePaused

	{

		get

		{

			return this.gamePaused;

		}


		set

		{

			this.gamePaused = value;

		}

	}
	//===========================
	public UnityEngine.InputSystem.Controls.KeyControl PausKey

	{

		get

		{

			return pausKey;

		}

		set

		{

			pausKey = value;

		}

	}
	//===========================

	#endregion


}
