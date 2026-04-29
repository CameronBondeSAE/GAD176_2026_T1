using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Sabre.AI
{
public class BoxOBJReference : MonoBehaviour
{
    // this is to be placed on the Box station
    public GameObject boxRef;
    public bool BoxSafe;
    public bool BoxInOpen;
    public float distanceSensitivity = 3;
    public List<GuardSense> BoxGuards = new List<GuardSense>();

    public void CheckBoxSafety()
    {
        float currentdist = Vector3.Distance(boxRef.transform.position, this.gameObject.transform.position);
        Debug.Log("Current distance box is from station is: " + currentdist);
        if( currentdist <= distanceSensitivity)
        {
            BoxSafe = true;
            //CheckBoxOutInOpen();
        }
        else
        {
            BoxSafe = false;
        }
    }

    public void CheckBoxOutInOpen()
    {
        BaseCollectable boxCollectRef = boxRef.GetComponent<BaseCollectable>();
        if(boxCollectRef != null)
        {
            if(boxCollectRef.Owner == null)
            {
                BoxInOpen = true;
            }
            else
            {
                BoxInOpen = false;
            }
        }
        else
        {
            Debug.Log("assigned box reference doesn't have collectable");
        }
    }

    public void OverrideBoxSafety()
    {
        BoxInOpen = true;
        BoxSafe = true;
    }


}
}