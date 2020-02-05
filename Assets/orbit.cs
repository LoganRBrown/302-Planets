using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class orbit : MonoBehaviour
{

    public Transform orbitCenter;
    public LineRenderer orbitPath;
    public bool oppositeOrbit;
    public float radius = 15;
    [Range(4, 32)] public int resolution = 8;
    [Range(0, 300)] public int orbitDelay;
    public float orbitSpeed = 1;
    public bool isPaused = false;
    public bool fastForward = false;
    public bool rewind = false;

    bool hasBeenFF = false;
    bool rewindActive = false;


    void Start()
    {
        orbitPath = GetComponent<LineRenderer>();

        orbitPath.loop = true;

        if (tag == "moon") orbitPath.enabled = false;

    }

    /* TODO:
     * 
     * Make the moons not coplaner(Tilt so they aren't orbitting the same axis as their planets.)
     * 
     * 
     * 
     * */

    void Update()
    {
        if (isPaused) return;

        if (rewind && !rewindActive)
        {
            oppositeOrbit = !oppositeOrbit;
            rewindActive = true;
        }
        if (!rewind && rewindActive) oppositeOrbit = !oppositeOrbit;

        if (fastForward && !hasBeenFF)
        {
            orbitSpeed *= 5;
            hasBeenFF = true;
        }

        if (!fastForward && hasBeenFF)
        {
            orbitSpeed /= 5;
            hasBeenFF = false;
        }

        if (tag == "moon")
        {
            //Vector3 pos = FindOrbitPoint(Time.time, radius);

            //transform.RotateAround(pos, Vector3.up, 1 * Time.deltaTime);

            Vector3 moonPos = FindOrbitPoint(Time.time, radius);

            transform.position = moonPos;
        }

        else
        {
            Vector3 pos = FindOrbitPoint(Time.time, radius);

            transform.position = pos;

            UpdatePoints();
        }

    }

    private Vector3 FindOrbitPoint(float angle, float mag)
    {
        Vector3 pos = (orbitCenter != null) ? orbitCenter.position : Vector3.zero;

        if (!oppositeOrbit)
        {

            pos.x += Mathf.Cos((angle * orbitSpeed) + orbitDelay) * mag;
            pos.z += Mathf.Sin((angle * orbitSpeed) + orbitDelay) * mag;
        }

        if (oppositeOrbit)
        {
            pos.x += Mathf.Sin((angle * orbitSpeed) + orbitDelay) * mag;
            pos.z += Mathf.Cos((angle * orbitSpeed) + orbitDelay) * mag;
        }
        return pos;
    }

    private Vector3 FindLinePoints(float angle, float mag)
    {
        Vector3 pos = (orbitCenter != null) ? Vector3.zero : pos = orbitCenter.position;

        if (!oppositeOrbit)
        {

            pos.x += Mathf.Cos(angle) * mag;
            pos.z += Mathf.Sin(angle) * mag;
        }

        if (oppositeOrbit)
        {
            pos.x += Mathf.Sin(angle) * mag;
            pos.z += Mathf.Cos(angle) * mag;
        }

        return pos;
    }

    void UpdatePoints()
    {

        Vector3[] points = new Vector3[resolution];

        for(int i = 0; i < points.Length; i++)
        {
            //calculate x,y,z

            float p = i / (float)points.Length;
            float radians = p * Mathf.PI * 2;
            points[i] = FindLinePoints(radians, radius);

        }

        orbitPath.positionCount = resolution;
        orbitPath.SetPositions(points);
    }
}
