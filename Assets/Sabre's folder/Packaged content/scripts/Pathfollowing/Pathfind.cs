using UnityEngine;
using UnityEngine.AI;

namespace Sabre.AI
{
public class Pathfind : MonoBehaviour
{
    [SerializeField] private NavMeshPath path;
    public bool preTarget = true;
    public Transform targetTransform;
    public Vector3[] pathArray;
    public Vector3 targetNode;
    [SerializeField] private int cornerIndex;
    [SerializeField] private float minNodeDistance = 5;
    public TargetManager targetManager;

    void Awake()
    {
        NewTargeting();
    }

    public void FollowPath()
    {
        if(pathArray.Length <= 0 || targetTransform == null)
        {
            preTarget = false;
            NewTargeting();
            return;
        }
            else
        {
            if(cornerIndex < pathArray.Length)
            {

                if(Vector3.Distance(transform.position, pathArray[cornerIndex]) <= minNodeDistance)
                {
                    cornerIndex ++;

                    if(cornerIndex >= pathArray.Length)
                    {
                        return;
                    }
                }
                
                //Debug.Log("target corner index is: " + cornerIndex + " at: " + pathArray[cornerIndex]);
                //pathArray[cornerIndex] != TrackPath()[cornerIndex] || 
                if(cornerIndex > pathArray.Length)
                {
                    cornerIndex = 0;
                    //Debug.Log("path has been re-Calculated");
                    NewPathing();
                    return;
                           
                }
                else
                {
                    //Debug.Log("within array bounds, targeting corner: " + cornerIndex);
                    targetNode = new Vector3(pathArray[cornerIndex].x, transform.position.y, pathArray[cornerIndex].z);
                }

            }

            else
            {
                Debug.Log("Reached the final target");
                NewTargeting();
                return;
            }
        }
    }

    public void NewTargeting()
    {
        pathArray = null;
        if(preTarget == false)
        {
            NewPathing();
            return;
        }
        else
        {
            PickTarget();
        }
    }
    
    public void PickTarget()
    {
        if(targetManager == null)
        {
            targetManager = FindAnyObjectByType<TargetManager>();

            if(targetManager == null)
            {
                Debug.Log("Could not find any target manager");
                return;        
            }
        }

        targetTransform = targetManager.PickTarget(targetTransform);
        NewPathing();
        
    }
    
    private void NewPathing()
    {
        pathArray = TrackPath();
        cornerIndex = 0;
    }

    private Vector3[] TrackPath()
    {
        if( targetTransform == null)
        {
            return null;
        }
        path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, targetTransform.position, NavMesh.AllAreas, path);
        return path.corners;
    
       // Debug.Log("no path to attempt to calculate");
        //return null;
    }

    private void OnDrawGizmos()
    {
        Vector3[] drawPathArray = TrackPath();
        if(drawPathArray == null)
        {
            //Debug.Log("Path could not be found");
            return;
        }
        
        Vector3 CurrentNode = transform.position;

        foreach(Vector3 nodes in drawPathArray)
        {
            if(CurrentNode != transform.position)
            {
                Gizmos.DrawLine(CurrentNode, nodes);
            }
            CurrentNode = nodes;
        }
    }
}
}