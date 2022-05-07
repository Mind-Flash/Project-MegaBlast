using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadPlayer : MonoBehaviour
{
    #region /// Variables
    private bool bodyStanding;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

        bodyStanding = true;

    }

    #region /// CollisionDetection (When hit by projectile)
    void OnCollisionEnter2D(Collision2D collisionObject) 

    {
        if (collisionObject.gameObject.tag == "Projectile")  // If hit body fall over (new Sprite)

        {

            if (bodyStanding == true)

            { 

                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/SpriteFiddle_Death_Stage_002"); 

                bodyStanding = false;

            }

        }
    }
    #endregion

}
