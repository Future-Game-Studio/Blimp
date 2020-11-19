using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IsleType
{
    Resource,
    Empty,
    Default
}

public abstract class Isle : ScriptableObject
{
    public GameObject _prefab;
    public IsleType _type;
    public string _isleName;
    [TextArea(15, 20)]
    public string _description;
}
