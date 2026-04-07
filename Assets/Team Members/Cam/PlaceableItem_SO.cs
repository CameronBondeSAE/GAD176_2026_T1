using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable Item SO", menuName = "Alien Fingers/New Placeable Item")]
public class PlaceableItem_SO : ScriptableObject
{
	public int thing;
	public bool stuff;
	public Material material;
	public Texture2D texture;
	public AudioClip equipClip;
	public GameObject prefab;
}
