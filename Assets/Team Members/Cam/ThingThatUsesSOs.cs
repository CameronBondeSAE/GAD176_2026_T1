using UnityEngine;

public class ThingThatUsesSOs : MonoBehaviour
{
	public PlaceableItem_SO placeableItemSO;
	public int temp;
	
    void Update()
    {
		placeableItemSO.thing = temp;
    }
}
