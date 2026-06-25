using UnityEngine;
using UnityEngine.UI;

public class DepleteUI : MonoBehaviour
{
    public Slider statSlider;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
