using UnityEngine;

public class FOVDetection : MonoBehaviour
{
    [Header("Radius Detection")]
    [SerializeField, Tooltip("The init overlap to detect enemies inside FOV")]
    private float _detectionRadius;
    [SerializeField, Tooltip("The transform where to being the radius detection")]
    private Transform _detectionTransform;

    #if UNITY_EDITOR
    [SerializeField]
    private bool _drawDebug = true;
    #endif



#if UNITY_EDITOR


    private void OnDrawGizmosSelected()
    {
        if(_drawDebug && _detectionTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_detectionTransform.position, _detectionRadius);
        }
    }

#endif
}
