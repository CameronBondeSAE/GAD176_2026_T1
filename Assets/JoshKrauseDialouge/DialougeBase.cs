using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialougeBase : MonoBehaviour
{
    public CompanionCubeTrigger dialougeTrigger;
    public TextMeshProUGUI dialougeText;
    public int waitTime = 3;
   





    /* To add new npc dialouge
     * Make a new scirpt for them and replace 'MonoBehaviour' with 'DialougeBase'
     * Type 'public overide void Dialouge()' then press enter
     * Type 'Dialouge.text = ""' and enter your npc dialouge between the brackets
     * Give script to the object in the Hierarchy
     * Click onto the 'Dialouge Manager'cube so it appears in the inspector
     * Drag object into the list
     * Click onto object so it appears in the inspector
    */

    public virtual void Dialouge()
    {

        if ((Keyboard.current.tKey.wasPressedThisFrame) && (dialougeTrigger.canInteract == true))
        {
            dialougeText.enabled = true;

            StartCoroutine(Yield());
        }
    }

    public IEnumerator Yield()
    {

        yield return new WaitForSeconds(3);

        dialougeText.enabled = false;
    }

}