using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Divij
{
    public class Flare : MonoBehaviour, IInteractable
    {
    
        public float decreaseNumber = 0.5f;
        public Light flareLight;
        public float waitTime = 1f;

        public void Start()
        {
            flareLight = GetComponent<Light>();
        }


        // ReSharper disable Unity.PerformanceAnalysis
        public void Interact()
        {
            ActivateFlare();
        }

        private void ActivateFlare()
        {
            flareLight.intensity = 10f;
            Debug.Log("Flare Touched");
            StartCoroutine(FlareActivated());
        }
    
        IEnumerator FlareActivated()
        {
            while (flareLight.intensity > 0)
            {
                flareLight.intensity -= decreaseNumber;

                yield return new WaitForSeconds(waitTime);
            }
        
        }
    
        /*
         Start flare timer
         Once the flare's interact button is pressed it spawns the flare in at a set light value/Intensity
         The flare has a built in light intensity reduction formula to make it so that the light value decreases over time

         Interact(){
            UseFlareButton()
            }

            UseFlareButton(){
            Set Object's light intensity to a certain value


         */
        
        /*
         * Option 2
         *
         * For reasons the light wont decrease its intensity incrementally so Maybe hard code the light to decrease its intensity on start and just set
         * its value to something manually once interacted with. Wont be as flexible when applied to other objects such as match which would
         * have its own intensity and decrease values but might actually work
         */
    }

}
