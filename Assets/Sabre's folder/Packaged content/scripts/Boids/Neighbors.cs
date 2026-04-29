using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sabre.AI
{
    public class Neighbors : MonoBehaviour
    {
        public List<Transform> neighborsList = new List<Transform>();

        private void OnTriggerEnter(Collider neighborCol)
        {
            neighborsList.Add(neighborCol.gameObject.transform);
            Debug.Log("Approaching " + neighborCol.gameObject + " at position " + neighborCol.gameObject.transform);
        }

        private void OnTriggerExit(Collider neighborCol)
        {
            neighborsList.Remove(neighborCol.gameObject.transform);
            Debug.Log("leaving " + neighborCol.gameObject + " at position " + neighborCol.gameObject.transform);
        }
    }
}