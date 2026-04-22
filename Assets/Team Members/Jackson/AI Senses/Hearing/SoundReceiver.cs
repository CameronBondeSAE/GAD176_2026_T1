using UnityEngine;
using UnityEngine.Events;

namespace Team_Members.Jackson.AI_Senses.Hearing
{
    public class SoundReceiver : MonoBehaviour
    {
        public static UnityEvent ScaredSoundEvent = new();
    
        public void HeardSound(SoundTypes soundType)
        {
            ScaredSoundEvent.Invoke();
        }
    }
}