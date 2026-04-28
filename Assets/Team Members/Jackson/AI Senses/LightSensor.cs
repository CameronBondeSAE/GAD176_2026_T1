using System;
using Team_Members.Jackson.AI_Senses;
using  Divij;
using UnityEngine;

public class LightSensor : MonoBehaviour
{
    private AIPlayerSense _aiPlayerSense;
    private Divij.SwitchableLight _switchableLight;

    void Awake()
    {
        _aiPlayerSense = FindFirstObjectByType<AIPlayerSense>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _switchableLight = other.GetComponent<Divij.SwitchableLight>();

        if (_switchableLight != null && !_switchableLight.GetPowered())
        {
            _aiPlayerSense.isLit = false;
            _aiPlayerSense.playerWorking = false;
            _aiPlayerSense.foundBox = false;
            _aiPlayerSense.playerHasBox = false;
            _aiPlayerSense.foundCollector = false;
            _aiPlayerSense.boxDelivered = false;
        }
    }
}
