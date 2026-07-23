using System.Collections.Generic;
using UnityEngine;

public class SuntoLightsControls : MonoBehaviour
{

    public SunController Sc;
    
    private List<GameObject> switchableLightObjects = new List<GameObject>();
    private bool lightsAreOn = true;

    private void FindAllSwitchableLights()
    {
        switchableLightObjects.Clear();

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<Divij.SwitchableLight>() != null)
            {
                switchableLightObjects.Add(obj);
                Debug.Log("Found SwitchableLight object: " + obj.name);
            }
        }
        Debug.Log("Total SwitchableLight objects found: " + switchableLightObjects.Count);
    }

    

    private void UpdateLights()
    {
        bool shouldLightsBeOn = !(Sc.sunAngle > 10f && Sc.sunAngle < 170f);

        if (shouldLightsBeOn == lightsAreOn)
            return;

        lightsAreOn = shouldLightsBeOn;

        foreach (GameObject lightObj in switchableLightObjects)
        {
            if (lightObj != null)
            {
                lightObj.SetActive(shouldLightsBeOn);
            }
        }
    }

    void Start()
    {
        FindAllSwitchableLights();
    }

    void Update()
    {
        UpdateLights();
    }
}
