using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpeed : MonoBehaviour
{

    private float[] parentTransform;

    private string playerName;

    private Vector2 projectileVelocity; // Being set from "Projectile" Script in Awake


    #region /// Getter/Setters
    public Vector2 ProjectileVelocity
    

    {


        get

        {


            return projectileVelocity;

        }

        set


        {

            projectileVelocity = value;

        }



    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {


        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileVelocity.x, projectileVelocity.y);


        if (projectileVelocity.x != 50) // corrects in which direction the wind blows projectiles (if facing left)

        {

            ParticleSystem projectileParticleSystem = gameObject.GetComponent<ParticleSystem>();

            ParticleSystem.ForceOverLifetimeModule force = projectileParticleSystem.forceOverLifetime;

            force.enabled = true;

            force.xMultiplier = 3;


            gameObject.transform.position = new Vector2(gameObject.transform.position.x + 7.2f, gameObject.transform.position.y); // Corrects particle offset when turning around

            gameObject.GetComponent<Collider2D>().transform.position = new Vector2(gameObject.GetComponent<Collider2D>().transform.position.x - 2.5f, gameObject.GetComponent<Collider2D>().transform.position.y); // Flips collider for smoke when turning around

        }




        

    }

    // Update is called once per frame
    void Update()
    {

        Destroy(this.gameObject, gameObject.GetComponent<ParticleSystem>().main.duration); // Destroy particle object after particles have stopped playing
    }


    void OnCollisionEnter2D(Collision2D collisionObject) // Collisiondetection Enter (when hitting something)

    {

        if (collisionObject.gameObject.name != null && collisionObject.gameObject.tag != "Projectile")

        {
            playerName = collisionObject.gameObject.name; // checks name of the player whoms muzzle flame projectile spawns from
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x/2, 2); //change direction of particles when hitting target
            gameObject.GetComponent<Collider2D>().enabled = false;// makes sure particles can not be collided with after collision with player.
            gameObject.GetComponent<TrailRenderer>().emitting = false;
            
        }


    }   


}
