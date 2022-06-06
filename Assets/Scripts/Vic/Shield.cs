using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    GameObject myLeftSide;
    [SerializeField]
    GameObject myRightSide;

    SpriteRenderer myRenderer;


    
    EdgeCollider2D myCollider;

    bool myFacingRight;

    bool myIsActivated;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Start()
    {
        myFacingRight = true;
    }

    public void SetShield(bool anActivation, bool aFacingRight)
    {
        myIsActivated = anActivation;

        if (myIsActivated)
        {
            FlipShield(aFacingRight);
        }
        else
        {
            myRightSide.SetActive(false);
            myLeftSide.SetActive(false);
        }
    }

    public void FlipShield(bool aFacingRight) //Add ENUM for facingdirection
    {
        myFacingRight = aFacingRight;
        if (myFacingRight)
        {
            myRightSide.SetActive(true);
            myCollider = myRightSide.GetComponent<EdgeCollider2D>();
            myLeftSide.SetActive(false);
        }
        else if (!myFacingRight)
        {
            myRightSide.SetActive(false);
            myLeftSide.SetActive(true);
            myCollider = myLeftSide.GetComponent<EdgeCollider2D>();
        }
    }
}
