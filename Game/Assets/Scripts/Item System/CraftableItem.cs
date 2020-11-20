using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Craftable Item", menuName = "Inventory System/Items/Craftable", order = 51)]
public class CraftableItem : Item
{
    [SerializeField] private List<ItemSlot> _recipe;
    public List<ItemSlot> recipe { get { return _recipe; } }
    private void Awake()
    {
        _type = ItemType.Craftable;
    }
}
