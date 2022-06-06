using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float mySpeed = 9f;
    [SerializeField]
    float myMaxDistanceInSeconds = 3f;

    float myStartTime;
    bool myDirectionSet = false;
    Vector3 myDirection;

    void Start()
    {
        myStartTime = Time.realtimeSinceStartup;
        
    }

    void Update()
    {
        if (Time.realtimeSinceStartup - myStartTime > myMaxDistanceInSeconds)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (myDirectionSet)
            {
                transform.position += myDirection * mySpeed * Time.deltaTime;
            }

        }

    }

    public void SetDirection(Vector3 aDirection)
    {
        myDirection = aDirection;
        float degreeRotation = Mathf.Atan2(aDirection.y, aDirection.x) * Mathf.Rad2Deg;
        if (aDirection.x == 0 && aDirection.y > 0)
        {
            transform.position += Vector3.up;
        }
        else if (aDirection.x == 0 && aDirection.y < 0)
        {
            //transform.position -= Vector3.up;
        }

        transform.Rotate(0, 0, degreeRotation);
        myDirectionSet = true;
    }
}
