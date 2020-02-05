using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetModifiers : MonoBehaviour
{

    public Vector3 planetScale = new Vector3(1, 1, 1);
    public float planetRotationRateX = 0;
    public float planetRotationRateY = 0;
    public float planetRotationRateZ = 0;
    private Vector3 prevScale;

    void Start()
    {
        transform.Rotate(90.0f, 0, 0, Space.World);
    }

    
    void Update()
    {
        RotatePlanet();

        ScalePlanet();
    }

    private void RotatePlanet()
    {

        transform.Rotate(planetRotationRateX*Time.deltaTime,planetRotationRateY*Time.deltaTime,planetRotationRateZ*Time.deltaTime, Space.World);
 
    }

    private void ScalePlanet()
    { 
        if(planetScale != prevScale)
        {
            transform.localScale = planetScale;

            prevScale = planetScale;
        }
    }

}
