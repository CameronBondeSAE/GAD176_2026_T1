using UnityEngine;

public class soundEmitter : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    
    public void Emit()
    {
        Collider[] results = new Collider[50];

        Physics.OverlapSphereNonAlloc(transform.position, radius, results);


        foreach (Collider result in results)
        {
            if (result != null)
            {
                soundRecving soundRecving = result.GetComponent<soundRecving>();

                if (soundRecving != null)
                {
                    soundRecving.heardSound();
                }
            }
        }
    }
}
