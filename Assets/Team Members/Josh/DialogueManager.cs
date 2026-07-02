using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class DialougeManager : MonoBehaviour
{
    public DialougeBase dialougeText;
    public List<DialougeBase> objects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialougeText.dialougeText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CoroutineDialougeBase());
      
    }

    public IEnumerator CoroutineDialougeBase()
    {

        
        foreach (DialougeBase obj in objects)
        {
            obj.Dialouge();

        }
        yield return new WaitForSeconds(1);

    }
}
