using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DialougeManager : MonoBehaviour
{
    public DialogueBase dialougeText;
    public List<DialogueBase> objects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialougeText.dialogueText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CoroutineDialougeBase());
      
    }

    public IEnumerator CoroutineDialougeBase()
    {

        
        foreach (DialogueBase obj in objects)
        {
            obj.Dialogue();

        }
        yield return new WaitForSeconds(1);

    }
}
