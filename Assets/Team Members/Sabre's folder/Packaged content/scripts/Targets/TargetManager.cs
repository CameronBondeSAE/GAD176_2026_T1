using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sabre.AI
{
    public class TargetManager : MonoBehaviour
    {
        public List<Transform> targetList = new List<Transform>();

        public Transform PickTarget(Transform oldTargetTransform)
        {
            if(targetList.Count == 0)
            {
                Debug.Log("No targets in the target list to pick from");
                return null;
            }

            int targetIndex = Random.Range(0, targetList.Count - 1);
            
            Debug.Log("new Target chosen: " + targetList[targetIndex]);

            return targetList[targetIndex];
            
        }
    }
}