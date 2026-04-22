using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsComputer : MonoBehaviour
{
    public float effectScale = 5f;
    public float powerRequired = 10f;

    // public SoundPlayer_Networked SoundPlayerNetworked;
    
    public AudioSource audioSourceVoice;
    public AudioSource[] audioSourceSFX;

    public AudioClip poweringUp;
    public AudioClip bashingHull;
    
    private float[] audioData = new float[512]; // Change the array size to suit your needs

    public bool poweredState = false;

    public Transform view;

    public Color colour;// = new Color(0, average, average);

    
    void Update()
    {
        audioSourceVoice.GetOutputData(audioData, 0); // Get the audio data for the current frame
        float sum = 0f;
        for (int i = 0; i < audioData.Length; i++)
        {
            sum += Mathf.Abs(audioData[i]); // Calculate the absolute value of each sample
        }
        float average = sum / audioData.Length; // Calculate the average volume level for the frame
        // Debug.Log("Volume level: " + average.ToString("#.#"));

        average *= effectScale;
        view.transform.localScale = Vector3.one + new Vector3(average, average, average);
        Renderer rend = GetComponentInChildren<Renderer>();
        Material material = rend.material;

        // material.color = colour;


        material.SetColor("_Colour", colour);
        
        // material.SetColor("_EmissionColor", colour);
        // DynamicGI.SetEmissive(rend, colour);
        // material.
        // material.SetColor("_EmissionColor", new Color(0, average*100f, 0));
        // material.SetFloat("_EmissionIntensity", 100f);
        // material.EnableKeyword("_EMISSION");//This is a bug in unity


        view.localPosition = new Vector3(0,Mathf.Sin(Time.time*2f)/4f,0);
    }

    public float PowerRequired()
    {
        return powerRequired;
    }

    public void TurnOff()
    {
        poweredState = false;
        foreach (AudioSource a in audioSourceSFX)
        {
            a.Stop();    
        }
    }

    public void PowerSupplied(float power)
    {
        //Debug.Log("Computer got power : "+power + " : BUT I WANTED = "+powerRequired);

        if (poweredState == false && power >= powerRequired-0.01f)
        {
            poweredState = true;

            StartSequence();
        }
    }

    private void StartSequence()
    {
        StartCoroutine(VoiceSequence());
    }

    private IEnumerator VoiceSequence()
    {
        // Invoke("TurnOff", 30f);
        foreach (AudioSource a in audioSourceSFX)
        {
            a.Play();    
        }
        yield return new WaitForSeconds(8);
        
        audioSourceVoice.clip = poweringUp;
        audioSourceVoice.Play();
        yield return new WaitForSeconds(18);
        audioSourceVoice.clip = bashingHull;
        audioSourceVoice.Play();
    }
}
