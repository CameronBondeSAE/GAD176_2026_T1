using System;
using UnityEngine;

public class orangeGuy : MonoBehaviour
{
    public AudioSource audioSource;

    public soundEmitter SoundEmitter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.PlayOneShot(audioSource.clip);
        SoundEmitter.Emit();
    }

    private void Update()
    {
        
    }
}
