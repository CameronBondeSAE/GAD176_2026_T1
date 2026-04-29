using System.Collections;
using Anthill.AI;
using UnityEngine;

public class turntowardsPlayer : MonoBehaviour
{
    public Collectstates states;
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] Transform targetObject;
    [SerializeField] private float turnSpeed = 2f;
    [SerializeField] Vector3 targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir;
        
        if (targetObject)
        {
            // Has a target gameobject
            targetDir = (targetObject.position - transform.position).normalized;
            float angle = Vector3.SignedAngle(transform.forward, targetDir, transform.up);
            GetComponentInParent<Rigidbody>().AddRelativeTorque(0F,angle,0f);
            // rb.AddRelativeTorque(0f,angle,0f);
            StartCoroutine(drawWrapon());
        }
        
    }

    private IEnumerator drawWrapon()
    {
      yield return new WaitForSeconds(3f);
      GetComponentInParent<Collectstates>().isCanSeeplayer = true;
      GetComponentInParent<Collectstates>().isweapondDrawn = false;
      GetComponentInParent<Collectstates>().isLookingforplayer = false;
      // states.isCanSeeplayer = true;
      // states.isweapondDrawn = true;
      // states.isLookingforplayer = false;
    }
}
