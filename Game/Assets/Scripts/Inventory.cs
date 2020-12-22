using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private OwnedItems _items;
    public OwnedItems Items { get => _items; }

    public Inventory()
    {
        if (_items == null)
            _items = ScriptableObject.CreateInstance<OwnedItems>();
    }
}
