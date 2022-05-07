using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour


{

    #region /// Variables
    private float projectileSpeed; // "Gun shot" speed
	private float projectileLiveTime; // How long "Gun shot" will last before destroyed (seconds)
	private float VelocityY;


	private GameObject master;
	private GameObject Player;
	#endregion

	// Use this for initialization
	void Start () 
	
	
	{

		master = GameObject.Find("Master");
		VelocityY = gameObject.GetComponent<Rigidbody2D>().velocity.y;

		GetComponentInChildren<ParticlesSpeed>().ProjectileVelocity = new Vector2(projectileSpeed, VelocityY); // Setting velocity for particles so that they match projectile

		if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0) // if facing opposite direction change direction of projectileSmoke

        {

			GetComponentInChildren<ParticlesSpeed>().ProjectileVelocity = new Vector2(projectileSpeed * -1, VelocityY);

		}

		transform.DetachChildren(); // Detach particles so that they can continue playing even when projectile is destroyed 
		                            //(needs to be done here, if done when destroyed particles will be destroyed and reset at new location)

	}

	void Awake()


    {

		projectileLiveTime = 1;
		projectileSpeed = 50;

	}

	// Update is called once per frame
	void Update () 
	
	
	{

		Destroy(this.gameObject, projectileLiveTime); // After "projectileLiveTime" destroy projectile
	}

    #region /// CollisionDetection when projectile hits something
    void OnCollisionEnter2D(Collision2D collisionObject) 
    {

		if (collisionObject.gameObject.tag == "Projectile")

		{

			GameObject ProjectileBlastInstantiated = Instantiate(Resources.Load("Prefabs/ProjectileExplosion") as GameObject, this.transform.position, transform.rotation);
			Destroy(this.gameObject);

		}

		if (collisionObject.gameObject.tag == "Player")

        {

			GameObject ProjectileBlastInstantiated = Instantiate(Resources.Load("Prefabs/ProjectileExplosion") as GameObject, this.transform.position, transform.rotation);
			Destroy(this.gameObject);

		}

		if (collisionObject.gameObject.tag == "PlayerDead")

		{

			GameObject ProjectileBlastInstantiated = Instantiate(Resources.Load("Prefabs/ProjectileExplosion") as GameObject, this.transform.position, transform.rotation);
			Destroy(this.gameObject);

		}

    }
    #endregion

    #region /// Getter/Setters
    public float ProjectileSpeed

	{

		get

		{

			return this.projectileSpeed;

		}

		set

		{

			this.projectileSpeed = value;

		}

	}
    #endregion

}
