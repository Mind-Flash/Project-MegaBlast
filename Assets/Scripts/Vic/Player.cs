using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    int myJumpForce = 3;
    [SerializeField]
    float myMoveSpeed = 5f;

    bool myIsGrounded = false;
    bool myIsMoving = false;
    bool mySpriteIsFlipped = false;
    bool myIsGrabbing = false;

    Vector3 myMovementDirection;
    Rigidbody2D myBody;
    PlayerInput myInput;
    SpriteRenderer mySpriteRenderer;
    Animator myAnimator;

    EdgeHandler myEdge;
    
    [SerializeField]
    GameObject myFacingRight;
    [SerializeField]
    GameObject myFacingLeft;
    [SerializeField]
    GameObject myAim;
    [SerializeField]
    GameObject myProjectile;


    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myInput = GetComponent<PlayerInput>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myMovementDirection = Vector3.zero;
        FlipCharacter();
    }

    private void Update()
    {
        MovePlayer();
    }

    public void Move(InputAction.CallbackContext aCallbackContext)
    {
        float movementInput = aCallbackContext.ReadValue<float>();
        if (aCallbackContext.phase == InputActionPhase.Performed)
        {
            myIsMoving = true;
            myMovementDirection.x = movementInput;
            Debug.Log("Moving");
        }

        if (aCallbackContext.phase == InputActionPhase.Canceled)
        {
            myIsMoving = false;
            if (myIsGrounded)
            {
                myMovementDirection = Vector3.zero;
            }
        }
        myAnimator.SetFloat("Speed", Mathf.Abs(movementInput));
    }

    public void Jump(InputAction.CallbackContext aCallbackContext)
    {
        if (myIsGrounded && aCallbackContext.phase == InputActionPhase.Started)
        {
            myIsGrounded = false;
            myBody.AddForce(Vector2.up * myJumpForce, ForceMode2D.Impulse);
            myAnimator.SetBool("IsJumping", true);
        }
        Debug.Log("Jumping");
    }

    public void Grab(InputAction.CallbackContext aCallbackContext)
    {
        if (myEdge != null && aCallbackContext.phase == InputActionPhase.Started)
        {
            Debug.Log("Grabbbing");
            Grab();
        }
    }

    public void PullUp(InputAction.CallbackContext aCallbackContext)
    {
        if (myIsGrabbing && aCallbackContext.phase == InputActionPhase.Started)
        {
            Debug.Log("Pulling Up");
            PullUp();
        }
    }

    public void Drop(InputAction.CallbackContext aCallbackContext)
    {
        if (myIsGrabbing && aCallbackContext.phase == InputActionPhase.Started)
        {
            Debug.Log("Dropping down");
            myBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector3(transform.position.x, transform.position.y - .1f);
            myIsGrabbing = false;
        }
    }

    public void Shoot(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Started)
        {
            GameObject bullet = Instantiate<GameObject>(myProjectile, myAim.transform.position, myAim.transform.rotation);
            Vector3 position = myAim.transform.position - transform.position;
            
            bullet.GetComponent<Bullet>().SetDirection(position);
        }
        Debug.Log("Peew peeeew!!!");
    }

    public void Shield(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Performed)
        {
            Debug.Log("Shielded!!");
        }

        if (aCallbackContext.phase == InputActionPhase.Canceled)
        {
            Debug.Log("UnShielded!!!");
        }
    }

    public void Aim(InputAction.CallbackContext aCallbackContext)
    {
        if (aCallbackContext.phase == InputActionPhase.Performed)
        {
            Debug.Log(aCallbackContext.ReadValue<Vector2>().normalized);
            Vector2 direction = aCallbackContext.ReadValue<Vector2>().normalized;
            Vector3 aimDirection = new Vector3(direction.x, direction.y, 0);
            myAim.transform.position = transform.position + aimDirection*2;
            if (aimDirection.x < 0)
            {
                mySpriteIsFlipped = true;
            }
            if (aimDirection.x > 0)
            {
                mySpriteIsFlipped = false;
            }
            FlipCharacter();
        }
    }

    void MovePlayer()
    {
        if (myIsGrabbing)
        {
            transform.position += Vector3.zero;
        }
        else if (myIsMoving || !myIsGrounded)
        {
            transform.position += myMovementDirection * myMoveSpeed * Time.deltaTime;
        }

    }

    void FlipCharacter()
    {
        if (mySpriteIsFlipped)
        {
            mySpriteRenderer.flipX = true;
            myFacingRight.SetActive(false);
            myFacingLeft.SetActive(true);
        }
        else
        {
            mySpriteRenderer.flipX = false;
            myFacingRight.SetActive(true);
            myFacingLeft.SetActive(false);
        }
    }

    public void SetGrabable(EdgeHandler anEdge)
    {
        myEdge = anEdge;
    }

    void Grab()
    {
        myIsGrabbing = true;
        transform.position = myEdge.GetHangingposition();
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void PullUp()
    {
        transform.position = myEdge.GetStandUpPosition();
        myBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        myIsGrabbing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !myIsGrounded)
        {
            Debug.Log("Grounded");
            myIsGrounded = true;
            if (!myIsMoving)
            {
                myMovementDirection = Vector3.zero;
            }
            myAnimator.SetBool("IsJumping", false);
        }
    }

}
