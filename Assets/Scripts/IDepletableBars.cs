using UnityEngine;

public interface IDepletableBars
{
    void UiDisplayUpdate();
    int MaxValue();
    int MinValue();
    int CurrentValue();
    
}
