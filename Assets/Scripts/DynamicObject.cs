using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DynamicType
{
    Player,
    Reactor,
    Wall,
    Door
}

public class DynamicObject : MonoBehaviour
{
    public DynamicType type;
}