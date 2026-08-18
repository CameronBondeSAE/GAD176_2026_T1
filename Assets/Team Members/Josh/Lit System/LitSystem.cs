using UnityEngine;
using Divij;
using UnityEngine.Serialization;

public class LitSystem : MonoBehaviour
{
    [SerializeField] int litAmount = 0;
    public BoxCollider lightHitbox;
    public SwitchableLightModel switchableLightModel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (switchableLightModel.isPowered.Value)
        {
            lightHitbox.enabled = true;
        }

        if (!switchableLightModel.isPowered.Value)
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
