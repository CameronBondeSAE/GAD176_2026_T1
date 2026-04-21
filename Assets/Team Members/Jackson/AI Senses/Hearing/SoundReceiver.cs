using UnityEngine;
using UnityEngine.Events;

public class SoundReceiver : MonoBehaviour
{
    public static UnityEvent ScaredSoundEvent = new();
    
    public void HeardSound(SoundTypes soundType)
    {
        ScaredSoundEvent.Invoke();
    }
}