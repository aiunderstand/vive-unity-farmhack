using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AzureSky;
using VRTK;

public class DataManager : MonoBehaviour {

    public GameObject data0;
    public GameObject data1;
    public GameObject data2;
    int index = 0;
    public float touchAxisThreshold = 0.75f;
    GameObject[] data = new GameObject[3];
    public AzureSkyController sky;
    int hours = 8;

    private VRTK_ControllerEvents controllerEvents;

    // Use this for initialization
    void Awake () {
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        data[0] = data0;
        data[1] = data1;
        data[2] = data2;

        index = 0;
    }

    private void Start()
    {
        controllerEvents.TriggerPressed += new ControllerInteractionEventHandler(OnTriggerPressed);
        controllerEvents.TouchpadPressed += new ControllerInteractionEventHandler(OnTouchpadPressed);
    }

    private void OnDestroy()
    {
        controllerEvents.TriggerPressed -= new ControllerInteractionEventHandler(OnTriggerPressed);
        controllerEvents.TouchpadPressed += new ControllerInteractionEventHandler(OnTouchpadPressed);
    }

    private void OnTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        //disable current data
        data[index].SetActive(false);
      
        //goto next data
        index++;

        if (index >= 3)
            index = 0;

        //enable current data
        data[index].SetActive(true);
    }

    private void OnTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        Vector2 touchLocation = e.touchpadAxis;
        if (touchLocation.x > touchAxisThreshold)
        {
            // Right 
            hours++;

            if (hours > 24)
                hours = 24;

            sky.timeOfDay.GotoTime(hours, 0);
        }
        else if (touchLocation.x < -touchAxisThreshold)
        {
            // Left
            hours--;

            if (hours < 0)
                hours = 0;

            sky.timeOfDay.GotoTime(hours, 0);           
        }
        else if (touchLocation.y > touchAxisThreshold)
        {
            // Up
            sky.SetNewWeatherProfile(6);
        }
        else if (touchLocation.y < -touchAxisThreshold)
        {
            // Down
            sky.SetNewWeatherProfile(0);
        }
        else
        {
            // Center
           
        }
    }
}
