using System.Collections.Generic;
using UnityEngine;
using System.Text;

[CreateAssetMenu(fileName = "New Craftable Item", menuName = "Inventory System/Items/Craftable", order = 51)]
public class CraftableItem : Item
{
    [SerializeField] private List<ItemRecipe> _recipe;
    [SerializeField] private int _craftTime;
    [SerializeField] private int _maxOrder;
    public List<ItemRecipe> Recipe { get => _recipe; }
    public int CraftTime { get => _craftTime; }
    public int MaxOrder { get => _maxOrder; }

    

    public override string ColouredName => throw new System.NotImplementedException();

    private void Awake()
    {
        _type = ItemType.Craftable;
    }

}

[System.Serializable]
public class ItemRecipe
{
    [SerializeField] private Item _item;
    [SerializeField] private int _amount;
    public Item Item { get => _item; }
    public int Amount { get => _amount; }
}
