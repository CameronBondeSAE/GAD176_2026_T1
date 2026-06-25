using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class StaminaSys : MonoBehaviour, IDepletableBars
{
    public int staminaMax;
    public int staminaCurrent;
    public int staminaMin;
    public Slider staminaDisplay;
    public Rigidbody attachedEntity;

    public void Start()
    {
        attachedEntity = GetComponent<Rigidbody>();
    }

    public UnityEvent OnDepletion = new UnityEvent();

    private void OnEnable()
    {
        //OnDepletion.AddListener();
    }

    private void OnDisable()
    {
        
    }

    public void UiDisplayUpdate()
    {
        
    }

    public int MaxValue()
    {
        return staminaMax;
    }

    public int MinValue()
    {
        return staminaMax - staminaMax;
    }

    public int CurrentValue()
    {
        return staminaCurrent;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
