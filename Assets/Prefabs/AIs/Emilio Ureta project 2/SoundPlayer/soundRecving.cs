using System.Collections;
using UnityEngine;

public class soundRecving : MonoBehaviour
{
        public delegate void SoundHandler();
        public  event SoundHandler heardSound_Event;

    
        public void heardSound()
        {
         heardSound_Event?.Invoke();   
        }
}
