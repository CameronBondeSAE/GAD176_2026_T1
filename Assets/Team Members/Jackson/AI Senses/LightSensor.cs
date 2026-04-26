using System;
using Team_Members.Jackson.AI_Senses;
using UnityEngine;

public class LightSensor : MonoBehaviour
{
    private Divij.SwitchableLight _switchableLight;
    private AIPlayerSense _aiPlayerSense;

    void Awake()
    {
        _switchableLight = GetComponentInParent<Divij.SwitchableLight>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _aiPlayerSense = other.GetComponent<AIPlayerSense>();
        
        if (!_switchableLight.isPowered)
        {
            Debug.Log("Not lit");
            _aiPlayerSense.isLit = false;
        }
    }
}
