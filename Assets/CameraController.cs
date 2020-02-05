using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour
{
    public Canvas mainHUD;

    public GameObject sun;

    public EventSystem events;

    public Button play;
    public Button pause;
    public Button fastForward;
    public Button rewind;

    bool doingRR = false;
    bool doingFF = false;

    GameObject[] planets;

    GameObject[] buttons;

    private Transform focusTarget;
    private float smoothSpeed = 1f;
    private Vector3 offset = new Vector3(0,2,-20);

    void Start()
    {
        PlanetSetup();
    }

    void Update()
    {
        if (focusTarget != null)
        {
            transform.LookAt(focusTarget);

            Vector3 desiredPosition = focusTarget.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }    
    }

    void PlanetSetup()
    {

        buttons = GameObject.FindGameObjectsWithTag("button");
        planets = GameObject.FindGameObjectsWithTag("planet");

        for(int i = 0; i<=buttons.Length; i++)
        {

            if (buttons[i].GetComponent<ButtonController>().isSun)
            {
                buttons[i].GetComponent<ButtonController>().Init("Sun", sun, SunClicked);
            }
            else
            {
                buttons[i].GetComponent<ButtonController>().Init(planets[i - 1].name, planets[i - 1], PlanetBttnClicked);
            }
        }

    }

    public void SunClicked()
    {
        transform.LookAt(sun.transform);

        focusTarget = sun.transform;
    }

    public void PlanetBttnClicked()
    {
        Transform target = events.currentSelectedGameObject.GetComponent<ButtonController>().myPlanet.transform;

        focusTarget = target;
    }

    public void BttnPlayClicked()
    {
        for (int i = 0; i <= planets.Length; i++)
        {
            planets[i].GetComponent<orbit>().isPaused = false;
        }
    }
    public void BttnPauseClicked()
    {
        for(int i=0; i <= planets.Length; i++)
        {
            planets[i].GetComponent<orbit>().isPaused = true;
        }
    }
    public void BttnFFClicked()
    {
        if (doingFF)
        {
            for (int i = 0; i <= planets.Length; i++)
            {
                planets[i].GetComponent<orbit>().fastForward = false;
            }

            doingFF = false;
            return;
        }

        if (!doingFF)
        {
            for (int i = 0; i <= planets.Length; i++)
            {
                planets[i].GetComponent<orbit>().fastForward = true;
            }

            doingFF = true;
        }
    }
        public void BttnRRClicked()
    {
        if (doingRR)
        {
            for (int i = 0; i <= planets.Length; i++)
            {
                planets[i].GetComponent<orbit>().rewind = false;
            }

            doingRR = false;
            return;
        }

        if (!doingRR)
        {
            for (int i = 0; i <= planets.Length; i++)
            {
                planets[i].GetComponent<orbit>().rewind = true;
            }

            doingRR = true;
        }
    }
}
