using UnityEngine;

public class Camable : MonoBehaviour, ICamable
{
    public void TouchCam()
    {
        Debug.Log("Touching Cam");
    }
}
