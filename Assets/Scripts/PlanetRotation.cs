using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{

    private GameObject planet;
    private float rotationSpeed;
    Vector3 planetVectorRotation;
    Quaternion planetQuarternionRotation;


    // Start is called before the first frame update
    void Start()
    {

         planet = this.gameObject;
         rotationSpeed = 5;

    }

    // Update is called once per frame


    private void FixedUpdate()
    {    
        planet.GetComponent<Transform>().Rotate(Vector3.up * (rotationSpeed * Time.deltaTime)); // Rotates planet around it's axis
    }

    void Update()
    {

    }
}
