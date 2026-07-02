using CameronBonde;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBase : MonoBehaviour, IInteractable
{
    public DialogueTrigger dialogueTrigger;
    public TextMeshProUGUI dialogueText;
    public int waitForSeconds;
    public int dialogueStart = 0; // Makes sure the dialogue only triggers one time

    





    /* To add new npc dialouge
     * Make a new scirpt for them and replace 'MonoBehaviour' with 'DialougeBase'
     * Type 'public overide void Dialouge()' then press enter
     * Type 'Dialouge.text = ""' and enter your npc dialouge between the brackets
     * Give script to the object in the Hierarchy
     * Click onto the 'Dialouge Manager'cube so it appears in the inspector
     * Drag object into the list
     * Click onto object so it appears in the inspector
    */

    public virtual void Dialogue()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            dialogueStart = 0;
            dialogueText.enabled = true;
            dialogueStart =+ 1;
            
        }
        /*
        if (dialogueStart == false)
        {
            dialogueText.enabled = false;
        }
        */
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}