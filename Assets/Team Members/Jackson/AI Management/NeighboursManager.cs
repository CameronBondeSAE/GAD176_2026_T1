using System.Collections.Generic;
using UnityEngine;

public class NeighboursManager : MonoBehaviour
{
    public List<Transform> neighboursList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI"))
        {
            neighboursList.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AI"))
        {
            neighboursList.Remove(other.transform);
        }
    }
    
}
