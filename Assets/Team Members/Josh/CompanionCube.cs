using CameronBonde;
using JetBrains.Annotations;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class CompanionCube : DialogueBase
{
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Dialogue()
    {
        base.Dialogue();
        if (dialogueStart == 1)
        {
            dialogueText.color = Color.red;
            dialogueStart += 1;
            StartCoroutine(Dialouge());
            //dialogueStart = false;
        }
      
    }

    public IEnumerator Dialouge()
    {
        Debug.Log("Test");
        yield return new WaitForSeconds(3);
        waitForSeconds = 3;
        dialogueText.text = "I am a cube";
        yield return new WaitForSeconds(3);
        dialogueText.text = "";
        yield return new WaitForSeconds(1);
        dialogueText.text = "I am friendly";
        yield return new WaitForSeconds(3);
        dialogueText.text = "";
        
    }





}
