using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private float radius = 20f;
    [SerializeField] private LayerMask soundReceiverMask;
    [SerializeField] private LayerMask obstacleMask;

    public void Emit(SoundTypes soundType)
    {
        Collider[] results = new Collider[50];

        Physics.OverlapSphereNonAlloc(transform.position, radius, results, soundReceiverMask);
        
        foreach (Collider result in results)
        {
            if (result != null)
            {
                SoundReceiver soundReceiver = result.GetComponent<SoundReceiver>();

                bool didItHitAnything = Physics.Linecast(transform.position, result.transform.position, obstacleMask);
                
                if (soundReceiver != null && !didItHitAnything)
                { 
                    soundReceiver.HeardSound(SoundTypes.Roaring);
                }
            }
        }
    }
}