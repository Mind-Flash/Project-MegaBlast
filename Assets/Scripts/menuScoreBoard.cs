using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuScoreBoard : MonoBehaviour 

{

	GameObject master;

	// Use this for initialization
	void Start () 
	
	{

		master = GameObject.Find("Master");

	}
	
	// Update is called once per frame
	void Update () 
	
	{

		master.GetComponent<MainSetup>().RestartGame();

	}
}
