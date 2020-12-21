using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlimpProperties : MonoBehaviour
{
    public OwnedItems OwnedItems { private set; get; }

    void Start()
    {
        OwnedItems = ScriptableObject.CreateInstance<OwnedItems>();
    }

    void Update()
    {

    }
}
