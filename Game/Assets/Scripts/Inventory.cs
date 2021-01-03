using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public OwnedItems Items { get; private set; }
    public int MaxWeight { get; private set; }
    public int CurrentWeight { get => Items.SummaryWeight; }
    public int RemainderWeight { get => MaxWeight - Items.SummaryWeight; }
    public Inventory()
    {
        MaxWeight = 125;
        if (Items == null)
            Items = ScriptableObject.CreateInstance<OwnedItems>();
    }

    public void Add(Item item, int count)
    {
        Items.AddItem(item, count);
    }

    public void Remove(Item item, int count)
    {
        Items.RemoveItem(item, count);
    }

}
