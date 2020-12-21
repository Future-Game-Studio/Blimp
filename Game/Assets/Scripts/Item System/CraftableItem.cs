using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "New Craftable Item", menuName = "Inventory System/Items/Craftable", order = 51)]
public class CraftableItem : Item
{
    [SerializeField] private List<ItemSlot> _recipe;
    public List<ItemSlot> Recipe { get { return _recipe; } }

    public override string ColouredName => throw new System.NotImplementedException();

    private void Awake()
    {
        _type = ItemType.Craftable;
    }

}
