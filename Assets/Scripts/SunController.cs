using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SunController : MonoBehaviour
{
    public Transform Sun;
    public int DayNumber = 1;
    public bool IsDay;
    public float sunAngle;
    public float rotationAmount;
    
    public bool endgame = false;
    public UnityEvent EndGame;
    
    public bool AliensRun = false;
    public GameObject AlienPrefab;
    
    // 1. Expose the GameObject list to the Unity Inspector
    public List<GameObject> targetObjects = new List<GameObject>();
    
    // 2. Make the positions list
    public List<Vector3> positions = new List<Vector3>();

    // 3. A public method to retrieve all Vector3 positions at any given moment
    public void GetObjectPositions()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null) // Avoid null reference errors if a slot is left empty
            {
                // Extract the Vector3 world position
                positions.Add(obj.transform.position); 
            }
        }
    }

    private void AlienKiller()
    {
        foreach (GameObject Prefab in GameObject.FindGameObjectsWithTag("Ai"))
        {
            GameObject Alien = GameObject.FindGameObjectWithTag("Ai");
            Destroy(Alien);   
        }
        
    }
    
    private void AlienMom()
    {
        for (int i = 0; i < AlienMomMap[DayNumber]; i++)
        {
            Instantiate(AlienPrefab, positions[Random.Range(0, positions.Count)], Quaternion.identity);
        }
    }

    public Dictionary<int, int> AlienMomMap = new Dictionary<int, int>()
    {
        { 1, 3 },
        { 2, 3 },
        { 3, 4 },
        { 4, 4 },
        { 5, 5 },
    };   
    
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
        GetObjectPositions();
    }

    // Update is called once per frame
    void Update()
    {
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

        sunAngle %= 360f;

        if (previousAngle > 350f && sunAngle < 10f)
        {
            DayNumber++;
        }

        if (IsDay == true)
        {
            AlienKiller();
            AliensRun = false;
        }
        else if (IsDay == false && AliensRun == false)
        {
            AlienMom();
            AliensRun = true;
        }
    }
}
