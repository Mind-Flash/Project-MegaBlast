using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Måste vara involverad för att kunna komma åt UI objekt

public class HealthUpdateP1 : MonoBehaviour

{

	//===========================
	// Variabler: 
	//===========================

	private int health;
	private int lives;
	private string healthText;
	Text guiHealthText;

	//===========================
	// Getter/Setters: 
	//===========================

	public int Health

	{
		get

		{

			return health;

		}


		set

		{

			health = value;

		}

	}
	//===========================

	public int Lives

	{
		get

		{

			return lives;

		}


		set

		{

			lives = value;

		}

	}

	// Use this for initialization
	void Start()

	{


		health = 3;
		lives = 3;
		guiHealthText = GetComponent<Text>();

		guiHealthText.text = "P1: HEALTH: " + health.ToString() + " Lives " + lives.ToString();

	}

	// Update is called once per frame
	void Update()

	{

	}


	public void UpdateGui()

    {

		guiHealthText.text = "P1: HEALTH: " + health.ToString() + " Lives " + lives.ToString();

	}


}


