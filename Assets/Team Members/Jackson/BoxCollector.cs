using System;
using Team_Members.Jackson.AI_Senses;
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
        if (other.gameObject.CompareTag("Player") && _playerSense.playerHasPowerUp)
        {
            _playerSense.pointsEarned++;
        }
    }
}
