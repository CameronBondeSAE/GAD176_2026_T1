using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Ray RayVar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo; // This is EMPTY. Raycast fills it in via the 'out' keyword
        bool raycastHitSomething = Physics.Raycast(transform.position, transform.forward, out hitInfo);

        if (raycastHitSomething)
        {
            // Shoot another ray, use the hitInfo.normal
            
            Physics.Raycast(hitInfo.point, hitInfo.normal, out hitInfo);
            Debug.Log("Shooting");
            Vector3 reflect = Vector3.Reflect(hitInfo.point, hitInfo.normal);
        }

        
    }

}
