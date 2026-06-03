using TMPro;
using UnityEngine;

public class MikeTestUI : MonoBehaviour
{
    public TextMeshProUGUI testValueText;

    MikeTest mikeTest;

    private void Start()
    {
        mikeTest = GameObject.FindGameObjectWithTag("MikeTest").GetComponent<MikeTest>();
    }

    public void UpdateTestValue()
    {
        testValueText.text = mikeTest.testValue.ToString();
    }
}
