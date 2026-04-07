using UnityEngine;

public class SoundPlayer_Networked : MonoBehaviour// : NetworkBehaviour
{
    public AudioClip[] sounds;
    public AudioSource audioSource;

    // [Rpc(SendTo.ClientsAndHost)]
    public void PlaySound_Rpc(bool random, int soundIndex)
    {
        if (!audioSource.isPlaying || audioSource.clip != sounds[soundIndex])
        {
            if (random)
            {
                audioSource.clip = sounds[Random.Range(0,sounds.Length)];
            }
            else
            {
                audioSource.clip = sounds[soundIndex];
            }

            audioSource.Play();
        }
    }
}