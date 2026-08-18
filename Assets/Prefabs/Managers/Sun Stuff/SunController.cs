using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Divij;
using Unity.Netcode;

public class SunController : NetworkBehaviour
{
    public Transform Sun;
    public int DayNumber = 1;
    public bool IsDay;
    public float sunAngle;
    public float rotationAmount;
    public bool endgame = false;
    public UnityEvent EndGame;
    
    public Dictionary<int, float> DayNOtoNightLength = new Dictionary<int, float>()
    {
        { 1, 1f },
        { 2, 0.75f },
        { 3, 0.5f },
        { 4, 0.25f },
        { 5, 0f },
    };
    public Dictionary<int, float> DayNOtoDayLength = new Dictionary<int, float>()
    {
        { 1, 1f },
        { 2, 1.25f },
        { 3, 1.5f },
        { 4, 1.75f },
        { 5, 2f },
    };

    void endeternal()
    {
        Debug.Log("Welcome to the ETERNAL MIDNIGHT");
    }

    private void OnEnable()
    {
        EndGame.AddListener(endeternal);
    }

    private void OnDisable()
    {
        EndGame.RemoveListener(endeternal);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sunAngle = Sun.eulerAngles.x;
        IsDay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsServer)
        {
            return;
        }
        
        float previousAngle = sunAngle;

        IsDay = sunAngle is > 0f and < 180f;

        if (sunAngle > 0 && sunAngle < 180) // It is daytime
        {
            rotationAmount = (DayNOtoDayLength[DayNumber] * Time.deltaTime);
            Sun.transform.Rotate(Vector3.right * rotationAmount);
            sunAngle += rotationAmount;
        }
        else // It is Nighttime
        {
            rotationAmount = (DayNOtoNightLength[DayNumber] * Time.deltaTime);
            if (Sun != null)
            {
	            Sun.transform.Rotate(Vector3.right * rotationAmount);
	            sunAngle += rotationAmount;

	            if (DayNOtoNightLength[DayNumber] == 0)
	            {
		            sunAngle = 270;
		            Sun.transform.rotation = Quaternion.Euler(sunAngle, 0, 0);
		            if (endgame == false)
		            {
			            EndGame.Invoke();
			            endgame = true;
		            }
	            }
            }
        }

        sunAngle %= 360f;

        if (previousAngle > 350f && sunAngle < 10f)
        {
            DayNumber++;
        }

        UpdateAngleRpc(sunAngle);
    }

    [Rpc(SendTo.ClientsAndHost, Delivery = RpcDelivery.Unreliable)]
    private void UpdateAngleRpc(float angle)
    {
        sunAngle = angle;

        if (Sun != null)
        {
            Sun.transform.rotation = Quaternion.Euler(0, 0, sunAngle);
        }
    }
}
