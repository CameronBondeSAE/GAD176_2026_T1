using System.Collections;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public float radius = 7;
    [Range(0, 360)] public float angle;

    public GameObject playerRef;
    
    [SerializeField] private LayerMask aiPlayerMask;
    [SerializeField] private LayerMask obstructionMask;
    
    public bool canSeePlayer;

    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }
    
    private IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FOVCheck();
        }
    }
    
    public void FOVCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, aiPlayerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}
