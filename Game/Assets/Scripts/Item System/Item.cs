using UnityEngine;

public enum ItemType
{
    Resource,
    Craftable,
    Equipment
}

public abstract class Item : ScriptableObject
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected ItemType _type;
    [SerializeField] protected string _itemName;
    [TextArea(15, 20)] [SerializeField] protected string _description;
    [SerializeField] protected int _weight;
    public Sprite Icon { get => _icon; }
    public ItemType Type { get => _type; }
    public string ItemName { get => _itemName; }
    public string Description { get => _description;  }
    public int Weight { get => _weight; }
}
