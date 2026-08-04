using UnityEngine;
using Divij;

public class LitSystem : MonoBehaviour
{
    [SerializeField] int litAmount = 0;
    public BoxCollider lightHitbox;
    public Divij.SwitchableLight switchableLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( switchableLight.isPowered == true)
        {
            lightHitbox.enabled = true;
        }

        if (switchableLight.isPowered == false)
        {
            lightHitbox.enabled = false;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        litAmount = litAmount + 1;
    }

    private void OnTriggerExit(Collider other)
    {
        litAmount = litAmount - 1;
    }



}
