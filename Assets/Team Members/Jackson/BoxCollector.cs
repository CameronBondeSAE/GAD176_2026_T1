using System;
using UnityEngine;

public class BoxCollector : MonoBehaviour
{
    private AIPlayerSense _playerSense;

    private void Awake()
    {
        _playerSense = FindFirstObjectByType<AIPlayerSense>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _playerSense.playerHasBox)
        {
            _playerSense.pointsEarned++;
        }
    }
}
