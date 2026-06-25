using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	[SerializeReference] 
    public List<DialogueItem_Base> items;

    public IEnumerator PlaySequence()
    {
        foreach (DialogueItem_Base item in items)
        {
            // Update TextMeshProUGUI
            Debug.Log(item.text);
            // yield for a bit, use a variable for the pacing
            yield return new WaitForSeconds(1f);
            // Set text to empty
            Debug.Log("");
            // yield for a bit, use a different variable for the blank pacing
            yield return new WaitForSeconds(1f);
        }
    }
}
