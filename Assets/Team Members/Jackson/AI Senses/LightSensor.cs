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

    private void OnTriggerStay(Collider other)
    {
        _switchableLight = other.GetComponent<Divij.SwitchableLight>();

        if (_switchableLight != null)
        {
            //_aiPlayerSense.isLit = _switchableLight.GetPowered();
            _aiPlayerSense.playerWorking = false;
            _aiPlayerSense.foundDispenser = false;
            _aiPlayerSense.playerHasPowerUp = false;
            _aiPlayerSense.foundInactivePowerUp = false;
            _aiPlayerSense.powerUpDelivered = false;
        }
    }
}
