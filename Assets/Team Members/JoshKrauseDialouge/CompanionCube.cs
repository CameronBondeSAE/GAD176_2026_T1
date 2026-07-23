using UnityEditor.Rendering;
using UnityEngine;

public class CompanionCube : DialougeBase
{
    public CompanionCubeTrigger companionCubeTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Dialouge()
    {

        base.Dialouge();
        if (companionCubeTrigger.canInteract == true)
        {
            dialougeText.text = "I am a cube";
        }


    }





}
