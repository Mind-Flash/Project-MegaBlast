using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Needed for Keyboard.current.Key.isPressed/Released (used instead of input.GetKey

public class PlayerControls : MonoBehaviour

{

    #region /// Variables
    private int airPushForce; // Determines how much "momentum forward when jumping" (as to not stop in tracks when stop moving in air) 

	private GameObject groundChecker; 
	private GameObject player;   // Player Character
	private GameObject master;  // Master object in scene (takes care of global values)

	[SerializeField]
	public GameObject myAimPosition;

	Rigidbody2D myBody;
	
	private bool facingRight; // value for direction faced
	private bool onGround; 
	private bool airPush; // used to determine if you will have a little push forward when jumping forward (works in tandem with "airPushForce",see Controls "Jumping for more") 
	private bool myCanGrab;
	public bool MyIsGrabbing { get; set; }

	private float moveDirection;
	private float movementSpeed; // Movement Speed in realtime
	private float jumpSpeed; // Determens force of jump thus both speed and height

	EdgeHandler myEdge;
	

	private Input_Controls inputControls; // Input Actions object (innehåller control layout)

	private Sprite[] spriteSheet;

	private MainSetup mainSetupScript; // Global values inherited from Master object

    #endregion

    #region /// Getter/Setters
    public bool FacingRight

    {

		get

		{

			return facingRight; 
						
		}

		set

        {

			facingRight = value;

        }

    }
	//===========================


	public bool OnGround

    {

		get

        {

			return onGround;


        }

		set

        {


			onGround = value;

        }

    }
	//===========================

	public bool AirPush

	{

		get

		{


			return airPush;

		}

		set


		{

			airPush = value;

		}

	}
	//===========================
	#endregion

	#region /// Methods/Functions

	#region /// Moving Note: all movements, nesting with "MovingLeft & MovingRight"
	private void Moving()

	{

	

		if (moveDirection == 0)

		{
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y); // returns velocity to '0' when not moving
			ResetIdleStance(onGround, player, spriteSheet);  // Takes following arguments: OnGround bool, Player gameObject, Spritesheet	

		}


		if (moveDirection == -1)

		{

			MovingLeft(player, movementSpeed, onGround, airPush); // Takes following arguments: Player gameObject, MovementSpeed float, OnGround bool, airpush bool

		}


		if (moveDirection == 1)

		{

			MovingRight(player, movementSpeed, onGround, airPush); // Takes following arguments:  Player gameObject, MovementSpeed float, OnGround bool, airpush bool

		}

	}
	//=============================
	#endregion

	#region ///MovingLeft

	public void MovingLeft(GameObject Player, float MovementSpeed, bool OnGround, bool AirPush)

	{


		if (Player.GetComponent<Animator>().GetBool("isLanding") == false) // Makes sure you can't directly move when landed (makes player more "weighty")

		{

			//Player.GetComponent<SpriteRenderer>().flipX = true; // character faces right direction // Note! Deprecated, must use localscale * -1 otherwise BoxColliders will not flip


			if (Player.transform.localScale.x > 0) // flipping character around if facing wrong direction

			{

				Player.transform.localScale = new Vector3(player.transform.localScale.x * -1, player.transform.localScale.y, player.transform.localScale.z); // Note! I have to use this instead of flip, otherwise I cannot mirror colliders when turning around

			}

			Player.GetComponent<PlayerControls>().FacingRight = false;


			//Below: Correcting collider when turning around
			//=========================================================================================================================================================================================


			if (Player.GetComponent<BoxCollider2D>().offset.x < 0) // flipping collider to match new direction


				Player.GetComponent<BoxCollider2D>().offset = Player.GetComponent<BoxCollider2D>().offset = new Vector2(Player.GetComponent<BoxCollider2D>().offset.x * -1, Player.GetComponent<BoxCollider2D>().offset.y);


			//=========================================================================================================================================================================================


			if (Player.GetComponent<Animator>().GetBool("isIdle") == false) // Only move if landing animation has stopped and can't move forward if idle anim is playing (making sure the player isn't "gliding forwards" after landing.

			{

				if (Player.GetComponent<Animator>().GetBool("isJumpMoving") == true && gameObject.transform.GetChild(1).GetComponent<WallDetector>().WallTouchLeft == false) //Only move left if not touching a wall to the left while jumping

				{

					//Player.GetComponent<Transform>().position = new Vector2(Player.GetComponent<Transform>().position.x + (MovementSpeed * Time.deltaTime) * -1, Player.GetComponent<Transform>().position.y); // Move towards input x value
					Player.GetComponent<Rigidbody2D>().velocity = new Vector2(MovementSpeed * -1, Player.GetComponent<Rigidbody2D>().velocity.y); // Note: Using this instead of transform.position as it creates clipping when colliding (forcing through objects)

				}

				else if (Player.GetComponent<Animator>().GetBool("isJumpMoving") == false && OnGround == true && Player.GetComponent<Animator>().GetBool("landAnimPlaying") == false)

				{

					//Player.GetComponent<Transform>().position = new Vector2(Player.GetComponent<Transform>().position.x + (MovementSpeed * Time.deltaTime) * -1, Player.GetComponent<Transform>().position.y); // Move towards input x value
					Player.GetComponent<Rigidbody2D>().velocity = new Vector2(MovementSpeed * -1, Player.GetComponent<Rigidbody2D>().velocity.y); // Note: Using this instead of transform.position as it creates clipping when colliding (forcing through objects)


				}


			}


			if (OnGround == true)

				Player.GetComponent<Animator>().SetBool("isRunning", true); // Setting animation state to running


			//Below: Moving in air
			//=========================================================================================================================================================================================


			if (OnGround == false) // Adds force when in air as to not stop dead in tracks when stopping movement

			{

				Player.GetComponent<Animator>().SetBool("isRunning", false);

				Player.GetComponent<Animator>().SetBool("isJumpMoving", true); //Set animation to JumpMoving state


				if (AirPush == true)

				{

					Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-airPushForce, 0));

					Player.GetComponent<PlayerControls>().AirPush = false;

				}

			}

		}

	}
	//=============================
	#endregion

	#region ///MovingRight
	public void MovingRight(GameObject Player, float movementSpeed, bool OnGround, bool AirPush)

	{

		if (Player.GetComponent<Animator>().GetBool("landAnimPlaying") == false) // Makes sure you can't directly move when landed (makes player more "weighty"

		{

			if (Player.transform.localScale.x < 0) // flipping character around if facing wrong direction

			{
				Player.transform.localScale = new Vector3(player.transform.localScale.x * -1, player.transform.localScale.y, player.transform.localScale.z); // Note! I have to use this instead of flip, otherwise I cannot mirror colliders when turning around }

				//Player.GetComponent<SpriteRenderer>().flipX = false; // character faces right direction // Note! Deprecated, must use localscale * -1 otherwise BoxColliders will not flip
			}
			Player.GetComponent<PlayerControls>().FacingRight = true;


			//Below: Correcting collider when turning around
			//=========================================================================================================================================================================================

			if (Player.GetComponent<BoxCollider2D>().offset.x > 0) // flipping collider to match new direction


				Player.GetComponent<BoxCollider2D>().offset = Player.GetComponent<BoxCollider2D>().offset = new Vector2(Player.GetComponent<BoxCollider2D>().offset.x * -1, Player.GetComponent<BoxCollider2D>().offset.y);


			//=========================================================================================================================================================================================


			if (Player.GetComponent<Animator>().GetBool("isIdle") == false) // Only move if landing animation has stopped and can't move forward if idle anim is playing (making sure the player isn't "gliding forwards" after landing.

			{

				if (Player.GetComponent<Animator>().GetBool("isJumpMoving") == true && gameObject.transform.GetChild(1).GetComponent<WallDetector>().WallTouchRight == false) //Only move left if not touching a wall to the left while jumping

				{

					//Player.GetComponent<Transform>().position = new Vector2(Player.GetComponent<Transform>().position.x + (movementSpeed * Time.deltaTime), Player.GetComponent<Transform>().position.y); // Move towards input x value
					Player.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, Player.GetComponent<Rigidbody2D>().velocity.y); // Note: Using this instead of transform.position as it creates clipping when colliding (forcing through objects)

				}



				else if (Player.GetComponent<Animator>().GetBool("isJumpMoving") == false && OnGround == true && Player.GetComponent<Animator>().GetBool("landAnimPlaying") == false)

				{

					//Player.GetComponent<Transform>().position = new Vector2(Player.GetComponent<Transform>().position.x + (movementSpeed * Time.deltaTime), Player.GetComponent<Transform>().position.y); // Move towards input x value
					Player.GetComponent<Rigidbody2D>().velocity = new Vector2(movementSpeed, Player.GetComponent<Rigidbody2D>().velocity.y); // Note: Using this instead of transform.position as it creates clipping when colliding (forcing through objects)

				}


			}


			if (OnGround == true)


				Player.GetComponent<Animator>().SetBool("isRunning", true); // Setting animation state to running


			//Below: Moving in air
			//=========================================================================================================================================================================================

			if (OnGround == false) // Adds force when in air as to not stop dead in tracks when stopping movement

			{

				Player.GetComponent<Animator>().SetBool("isRunning", false);

				Player.GetComponent<Animator>().SetBool("isJumpMoving", true); //Set animation to JumpMoving state

				if (AirPush == true)

				{

					Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(airPushForce, 0));


					Player.GetComponent<PlayerControls>().AirPush = false;

				}

			}

		}

	}
	//=============================
	#endregion

	#region /// Normalize/Idle
	public void ResetIdleStance(bool OnGround, GameObject Player, Sprite[] SpriteSheet)

	{

		if (OnGround == true) // If not moving set sprite to 0

			Player.GetComponent<Animator>().SetBool("isRunning", false); // Setting animation state to idle

		// Below: Fixes bug where idle anim doesn't trigger when unpausing - makes sure idle anim triggers when unpausing and not moving
		if (/*gameObject*/GameObject.Find("Master").GetComponent<MainSetup>().PausKey.wasReleasedThisFrame == false && /*gameObject*/GameObject.Find("Master").GetComponent<MainSetup>().GamePaused == false && Player.GetComponent<SpriteRenderer>().sprite == SpriteSheet[4])

		{



			Player.GetComponent<Animator>().SetBool("isRunning", false); // Setting animation state to idle

		}

	}

	//=============================
	#endregion

	#region /// Jump
	public void Jump(bool OnGround, GameObject Player, float JumpSpeed)

	{

		if (OnGround == true && Player.GetComponent<Animator>().GetBool("isLanding") == false && inputControls.Default.Shoot.triggered == false)

		{

			if (Player.GetComponent<Animator>().GetBool("hasLanded") == true)

			{

				Player.GetComponent<Animator>().SetBool("isRunning", false); // Exiting run animation when jumping


				Player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);

			}


			// Below: MoveJumping Right

			//=========================================================================================================================================================================================

			if (inputControls.Default.Jump.triggered == true && inputControls.Default.Move.ReadValue<float>() == 1 && Player.GetComponent<Animator>().GetBool("isJumpMoving") == false) // if running and jumping switch to JumpMove animation state

			{

				Player.GetComponent<Animator>().SetBool("isJumpMoving", true); //Set animation to jumpmove state
			}


			// Below: MoveJumping Left
			//=========================================================================================================================================================================================

			if (inputControls.Default.Jump.triggered == true && inputControls.Default.Move.ReadValue<float>() == -1 && Player.GetComponent<Animator>().GetBool("isJumpMoving") == false) // if running and jumping switch to JumpMove animation state

			{

				Player.GetComponent<Animator>().SetBool("isJumpMoving", true); //Set animation to jumpmove state

			}

			//=========================================================================================================================================================================================



			// Below: Regular Jump
			//=========================================================================================================================================================================================

			if (Player.GetComponent<Animator>().GetBool("isJumpMoving") == false && Player.GetComponent<Animator>().GetBool("hasLanded") == true && inputControls.Default.Move.ReadValue<float>() == 0)

			{

				Player.GetComponent<Animator>().SetBool("isJumping", true); // Setting animation state to jump if not jumpMoving

			}

		}

	}
	#endregion

	#region /// Shooting
	public void Shooting(GameObject Player, bool OnGround)

	{

		//	NOTE: Actual spawning of projectile is in "isBlasting" script whichis applied to all the blasting animation states

		if (inputControls.Default.Jump.triggered == false && Player.GetComponent<Animator>().GetBool("landAnimPlaying") == false) // Note, you can't jump and shoot at the same time

		{

			Player.GetComponent<Animator>().SetBool("isIdle", false);


			GameObject.Find("Master").GetComponent<MainSetup>().RandBlast = Random.Range(1, 4); // Randomise next blast anim

			// Below: Blasting on Ground
			//=========================================================================================================================================================================================

			if (OnGround == true)

			{

				Player.GetComponent<Animator>().SetBool("landAnimPlaying", true);

				switch (GameObject.Find("Master").GetComponent<MainSetup>().RandBlast) // Applying correct blast anim corresponding to number 1 = blast_001 etc...
				{
					case 1:
						Player.GetComponent<Animator>().SetBool("isBlasting_001", true);
						break;
					case 2:
						Player.GetComponent<Animator>().SetBool("isBlasting_002", true);
						break;
					case 3:
						Player.GetComponent<Animator>().SetBool("isBlasting_003", true);
						break;

				}

			}

			//======================================================
			// Below: Blasting in air
			//======================================================

			if (OnGround == false)


			{


				switch (GameObject.Find("Master").GetComponent<MainSetup>().RandBlast) // Applying correct blast anim corresponding to number 1 = blast_001 etc...
				{
					case 1:
						Player.GetComponent<Animator>().SetBool("isJumpBlasting_001", true);
						Player.GetComponent<Animator>().SetBool("isJumpBlasting_002", false);
						Player.GetComponent<Animator>().SetBool("isJumpBlasting_003", false);
						break;
					case 2:
						Player.GetComponent<Animator>().SetBool("isJumpBlasting_002", true);
						Player.GetComponent<Animator>().SetBool("isJumpBlasting_001", false);
						Player.GetComponent<Animator>().SetBool("isJumpBlasting_003", false);
						break;
					case 3:
						Player.GetComponent<Animator>().SetBool("isJumpBlasting_003", true);
						Player.GetComponent<Animator>().SetBool("isJumpBlasting_002", false);
						Player.GetComponent<Animator>().SetBool("isJumpBlasting_001", false);
						break;

				}

			}

		}

	}
	//============================
	#endregion

	#endregion

	#region ///Controller Input Events

	#region ///OnMove
	private void OnMove(InputValue value)

	{

		moveDirection = value.Get<float>();

	}
	//=============================
	#endregion

	#region ///OnJump
	private void OnJump()

	{

		if (master.GetComponent<MainSetup>().GamePaused == false)

		{

			Jump(onGround, player, jumpSpeed); // Takes following arguments: OnGround bool, Player gameObject, JumpSpeed float

		}

	}
	//=============================
	#endregion

	#region ///OnShoot
	private void OnShoot()

	{

		if (master.GetComponent<MainSetup>().GamePaused == false)

		{

			Shooting(player, onGround); // Takes following arguments: Player GameObject, OnGround bool

		}

	}

	#endregion

	#region ///OnStartPause
	private void OnStartPause()

	{

		master.GetComponent<MainSetup>().PausGame();
	}
	//============================
	#endregion

	void OnGrab()
    {
		if (myEdge != null)
        {
			Debug.Log("GRABBING!");
			if (MyIsGrabbing)
            {
				Drop();
            }
			else
            {
				Grab();
            }
        }
    }

	void OnPullUp()
    {
		if (MyIsGrabbing)
        {
			PullUp();
        }
    }
	#endregion

	private void Awake()
    
	{
		groundChecker = gameObject.GetComponentInChildren<GroundDetector>().gameObject;

		airPushForce = 1000;

		inputControls = new Input_Controls();

		myBody = GetComponent<Rigidbody2D>();

        inputControls.Default.Aim.performed += Aim_performed;
	}

    private void Aim_performed(InputAction.CallbackContext obj)
    {
		
		Vector2 aimDirection = obj.ReadValue<Vector2>();
		Aim(aimDirection);
        //throw new System.NotImplementedException();
    }

    public void OnEnable() // Needed for input controller

	{
		inputControls.Enable();
	}

	public void OnDisable() // Needed for input controller (disables all actions except unpausing

	{

		inputControls.Disable();

	}

	// Use this for initialization
	void Start()


	{

		player = gameObject; 
		master = GameObject.Find("Master");

		facingRight = true;

		if (gameObject.name == "P2") //Needed to correct the first blastanim when P2 (cannot be changed from joinEvents for some reason)

        {

			facingRight = false;
		}

		onGround = false;
		airPush = false;

		movementSpeed = 20;
		jumpSpeed = 70;

		moveDirection = 0;

		spriteSheet = Resources.LoadAll<Sprite>("Sprites/SpriteFiddle"); //Note: Has to be in folder "resources"

		mainSetupScript = GameObject.Find("Master").GetComponent<MainSetup>();

	}     

	// Update is called once per frame
	void Update()


	{	

		if (!mainSetupScript.GamePaused) //Run game as normal if not paused

		{
			//NOTE: MAYBE USE THE ONENABLE FUNCTIONS TO PAUSE INSTEAD OF ONMOVE ETC.

			Moving();

		}

	}

	public void SetGrabable(EdgeHandler anEdge)
    {
		myEdge = anEdge;
    }

	void Grab()
    {
		MyIsGrabbing = true;
		transform.position = myEdge.GetHangingposition();
		myBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

	void Drop()
    {
		myBody.constraints = RigidbodyConstraints2D.FreezeRotation;
		transform.position = new Vector3(transform.position.x, transform.position.y - .1f);
		MyIsGrabbing = false;
    }

	void PullUp()
    {
		MyIsGrabbing = false;
		transform.position = myEdge.GetStandUpPosition();
		myBody.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void Aim(Vector2 aDirection)
    {
		Vector2 direction = aDirection.normalized;
		Vector3 aimDirection = new Vector3(direction.x, direction.y, 0);
		myAimPosition.transform.position = transform.position + aimDirection;
		myAimPosition.transform.LookAt(transform);
	
		myAimPosition.transform.Rotate(new Vector3(180, 0, 0), Space.Self);
		
		Debug.Log(myAimPosition.transform.position);
    }

}
