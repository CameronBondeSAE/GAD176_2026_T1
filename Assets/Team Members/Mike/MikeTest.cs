using UnityEngine;

public class MikeTest : MonoBehaviour
{
    // create a test integer to store the value
    // can get this value outside the script 
    // can only be set inside the script
    public int testValue { get; private set; }

    public void IncreaseTestValue()
    {
        testValue++;
    }
}
