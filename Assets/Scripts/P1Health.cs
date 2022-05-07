using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Needed to access PlayerInput

public class P1Health : MonoBehaviour 

{

    #region /// Variables
    private GameObject p1GuiText;
	private GameObject master;
	private GameObject spawnPoint;

	private int health;
    #endregion

    // Use this for initialization
    void Start () 
	
	{
		p1GuiText = GameObject.Find("Text_P1Health");
		master = GameObject.Find("Master");
		spawnPoint = GameObject.Find("SpawnP1");

		health = 3;

		
	}

	#region /// Collision detection (when hit by projectile)
	void OnCollisionEnter2D(Collision2D collisionObject) // Om man blir prickad av en projectile...

    {


		if (collisionObject.gameObject.tag == "Projectile")

        {
			health--;

			p1GuiText.GetComponent<HealthUpdateP1>().Health--; // Uppdatera info till gui


			if (health == 0) // Reduce lives is health is 0

			{

				p1GuiText.GetComponent<HealthUpdateP1>().Lives --; // Uppdatera info till gui

				p1GuiText.GetComponent<HealthUpdateP1>().Health = health; // Uppdatera info till gui

				GameObject DeathInstantiated = Instantiate(Resources.Load("Prefabs/DeathModel") as GameObject, new Vector2(gameObject.GetComponent<BoxCollider2D>().transform.localPosition.x, gameObject.transform.position.y), transform.rotation);

				
				if (gameObject.transform.localScale.x < 0) // flip model to match player

				{

					DeathInstantiated.GetComponent<SpriteRenderer>().flipX = true;
				}
				

				if (p1GuiText.GetComponent<HealthUpdateP1>().Lives != 0) // Make sure you don't spawn a new model after end game

				{

					this.gameObject.name = "killedP1"; // renaming so that controller will be set to newly spawned model

					Destroy(gameObject.GetComponent<PlayerInput>()); // Needs to be destroyed for another player to be able to use controller input for new character (otherwise dead character counts as a player still)

					p1GuiText.GetComponent<HealthUpdateP1>().Health = 3; // Resets visual Health Score to 3	

				}



				Destroy(gameObject);

			}

			if (p1GuiText.GetComponent<HealthUpdateP1>().Lives == 0) // If no health, spawn death animation/model

			{

				p1GuiText.GetComponent<HealthUpdateP1>().UpdateGui(); // Uppdaterar gui:n

				master.GetComponent<MainSetup>().SetWinnerText();

				master.GetComponent<MainSetup>().ActivateScoreScreen();

			}


			p1GuiText.GetComponent<HealthUpdateP1>().UpdateGui(); // Uppdaterar gui:n

			BlinkingCharacter(); // sets character to red

		}

    }
    #endregion


    #region /// Methods/Functions

    public void BlinkingCharacter() // Makes character blink red when hit

    {

		IEnumerator Wait = WaitForSeconds();

		StartCoroutine(Wait);

	}
    #endregion

    #region /// Getter/Setters
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
    #endregion


    IEnumerator WaitForSeconds() // Time for blink intervals (see "BlinkingCharacter function")

	{

		float seconds = 0.15f;

		GetComponent<SpriteRenderer>().color = Color.red;

		yield return new WaitForSeconds(seconds);

		GetComponent<SpriteRenderer>().color = Color.white;

		yield return new WaitForSeconds(seconds);

		GetComponent<SpriteRenderer>().color = Color.red;

		yield return new WaitForSeconds(seconds);

		GetComponent<SpriteRenderer>().color = Color.white;

		yield return new WaitForSeconds(seconds);

		GetComponent<SpriteRenderer>().color = Color.red;

		yield return new WaitForSeconds(seconds);

		GetComponent<SpriteRenderer>().color = Color.white;

		StopAllCoroutines();

	}

}
