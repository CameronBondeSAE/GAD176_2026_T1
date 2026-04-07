using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPointType
{
	FireFighter,
	Arsonist,
	Police,
	Hospital
}

public class SpawnPoint : MonoBehaviour
{
	public SpawnPointType spawnPointType;
}
