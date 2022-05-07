using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBlast : MonoBehaviour 

{ 
	
	private Animator anim;

	// Use this for initialization
	void Start ()
	
	{

		anim = GetComponent<Animator>();
		anim.speed = 0.25f;


	}

	
	
	// Update is called once per frame
	void Update () 
	
	{

	}
	


		



}
