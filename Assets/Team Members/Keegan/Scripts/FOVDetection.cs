using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class FOVDetection : MonoBehaviour
{


    #if UNITY_EDITOR
    [SerializeField]
    private bool _drawDebug = true;
    #endif

    private void Update()
    {
        
        
    }

#if UNITY_EDITOR


    private void OnDrawGizmosSelected()
    {

    }

#endif
}
