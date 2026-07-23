using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SuntoTime : MonoBehaviour // Start is called once before the first execution of Update after the MonoBehaviour is created
{
    public float angle;
    
    public int modhours;
    public int minutes;
    
    public bool AMPM;
    public string AMPMString;


    public Dictionary<bool, string> AMPMMap = new Dictionary<bool, string>()
    {
        { true, "AM" },
        { false, "PM" },
    };
    
    public SunController SunManager;

    private void OnEnable()
    {
        SunManager.EndGame.AddListener(endeternal);
    }

    private void OnDisable()
    {
        SunManager.EndGame.RemoveListener(endeternal);
    }

    void endeternal()
    {
        Debug.Log($"Current Sun Time: {modhours:D2}:{minutes:D2} {AMPMString}");
    }

    void Update()
    {
        // Get the rotation angle around the X axis (0 to 360)
        int previousmin = minutes;
        angle = SunManager.sunAngle;

        // Map 0-360 degrees to a 24 hour float
        // Assuming 0 degrees = Sunrise, 90 = Noon, 180 = Sunset, etc.
        float rawTime = (angle / 360f) * 24f;
        float finalHour = (rawTime + 6f) % 24f; 

        int hours = Mathf.FloorToInt(finalHour);
        minutes = Mathf.FloorToInt((finalHour - hours) * 60f);
        
        modhours = (hours % 12);
        if (modhours == 0)
        {
            modhours = 12;
        }

        if (SunManager.sunAngle > 90 && SunManager.sunAngle < 270)
        {
            AMPM = false;
        }
        else
        {
            AMPM = true;
        }

        AMPMString = AMPMMap[AMPM];

        if (previousmin != minutes)
        {
            int done = 0;
            if (done == 0)
            {
                Debug.Log($"Current Sun Time: {modhours:D2}:{minutes:D2} {AMPMString}");
                done = 1;
            }
        }
    }
}

///Debug.Log($"Current Sun Time: {modhours:D2}:{minutes:D2} {AMPMString}");