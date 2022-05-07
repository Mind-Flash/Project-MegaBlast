using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isBlasting : StateMachineBehaviour
{
    #region /// Variables
    private GameObject Player;
	private float projectileHeight; // y position of projectile relative to character
    #endregion


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Player = animator.gameObject;

		Player.GetComponent<EdgeCollider2D>().enabled = false; // turns of edge collider as to not interfere with hit detection

        #region /// If not in idle mode or in landing animation, spawn projectile
        if (Player.GetComponent<Animator>().GetBool("isIdle") == false && Player.GetComponent<Animator>().GetBool("isLanding") == false)

		{

			Vector2 projectileSpawnSpeed; 

			if (Player.GetComponent<PlayerControls>().FacingRight == true)

			{

				Vector2 projectileSpawnPos = new Vector2(Player.GetComponent<BoxCollider2D>().transform.localPosition.x + -5 * -1, Player.transform.position.y + projectileHeight);

				GameObject shotInstantiated = Instantiate(Resources.Load("Prefabs/Projectile") as GameObject, projectileSpawnPos, Player.transform.rotation);

				shotInstantiated.tag = "Projectile";

				projectileSpawnSpeed = new Vector2(shotInstantiated.gameObject.GetComponent<Projectile>().ProjectileSpeed, 0);

				shotInstantiated.gameObject.GetComponent<Rigidbody2D>().velocity = projectileSpawnSpeed;

			} 

			else if (Player.GetComponent<PlayerControls>().FacingRight == false)

			{

				Vector2 projectileSpawnPos = new Vector2(Player.GetComponent<BoxCollider2D>().transform.localPosition.x + 5 * -1, Player.transform.position.y + projectileHeight);

				GameObject shotInstantiated = Instantiate(Resources.Load("Prefabs/Projectile") as GameObject, projectileSpawnPos, Player.transform.rotation);


				shotInstantiated.tag = "Projectile";

				projectileSpawnSpeed = new Vector2(shotInstantiated.gameObject.GetComponent<Projectile>().ProjectileSpeed *-1, 0);

				shotInstantiated.gameObject.GetComponent<Rigidbody2D>().velocity = projectileSpawnSpeed;

				shotInstantiated.GetComponent<SpriteRenderer>().flipX = true;

				shotInstantiated.GetComponent<BoxCollider2D>().offset = shotInstantiated.GetComponent<BoxCollider2D>().offset * -1;

			} 

		}

		#endregion
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)

	{

			if (Player.GetComponent<PlayerControls>().OnGround == true)

			{


				projectileHeight = 0.2f;


			}

			else if (Player.GetComponent<PlayerControls>().OnGround == false)

			{

				projectileHeight = 0.5f;

			}

	}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

		#region /// Adds smokeparticles at the edge of the weapon (hot after shooting ("Prime effect")
		if (Player.transform.localScale.x < 0) // Adjust position of aprticles depending if you look right or left

		{

			Vector2 hotSmokePos = new Vector2(Player.GetComponent<Transform>().localPosition.x - 1.35f, Player.GetComponent<Transform>().localPosition.y + 0.3f);

			GameObject particlesHotWeapon = Instantiate(Resources.Load("Prefabs/ParticlesHotWeapon") as GameObject, hotSmokePos, Player.transform.rotation);
			particlesHotWeapon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2);

		}

		else if (Player.transform.localScale.x > 0) // Adjust position of aprticles depending if you look right or left

		{

			Vector2 hotSmokePos = new Vector2(Player.GetComponent<Transform>().localPosition.x + 1.35f, Player.GetComponent<Transform>().localPosition.y + 0.3f);

			GameObject particlesHotWeapon = Instantiate(Resources.Load("Prefabs/ParticlesHotWeapon") as GameObject, hotSmokePos, Player.transform.rotation);
			particlesHotWeapon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2);

		}

        #endregion

    }






    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
