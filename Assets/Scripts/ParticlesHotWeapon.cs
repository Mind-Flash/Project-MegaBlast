using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesHotWeapon : MonoBehaviour
{

    private float projectileLiveTime;
    // Start is called before the first frame update

    private float countdown;


    void Start()
    {
        projectileLiveTime = 2; // sätter autodestruct till när projektilerna slutat spelas
        countdown  = 3f;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        // Nedan: fadear ut röken med tid

        countdown = countdown - 1 * Time.deltaTime;
        
        gameObject.GetComponent<TrailRenderer>().startColor = new Color(gameObject.GetComponent<TrailRenderer>().startColor.r, gameObject.GetComponent<TrailRenderer>().startColor.g, gameObject.GetComponent<TrailRenderer>().startColor.b, gameObject.GetComponent<TrailRenderer>().startColor.a - 0.4f * Time.deltaTime);
        gameObject.GetComponent<TrailRenderer>().endColor = new Color(gameObject.GetComponent<TrailRenderer>().endColor.r, gameObject.GetComponent<TrailRenderer>().endColor.g, gameObject.GetComponent<TrailRenderer>().endColor.b, gameObject.GetComponent<TrailRenderer>().endColor.a -0.4f * Time.deltaTime);
        
        if (countdown == 0 || countdown < 0)

        {

            gameObject.GetComponent<TrailRenderer>().emitting = false;

        }

        if (gameObject.GetComponent<TrailRenderer>().endColor.a == 0) // Destroy object shortly after particles aren't visible anymore (should be som delay as to not destroy still visible particles)

        { 

            Destroy(gameObject, projectileLiveTime); 

        }

    }

}
