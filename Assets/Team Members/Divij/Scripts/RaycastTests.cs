using UnityEngine;

public class RaycastTests : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo; // This is EMPTY. Raycast fills it in via the 'out' keyword
        bool raycastHitSomething = Physics.Raycast(transform.position, transform.forward, out hitInfo);
      
        if (raycastHitSomething)
        {
            Vector3 bouncePos = hitInfo.point;
            Vector3 bounceRef = Vector3.Reflect(transform.forward, hitInfo.normal);
            
            
            Physics.Raycast(bouncePos, bounceRef);
        }

        
    }
}

