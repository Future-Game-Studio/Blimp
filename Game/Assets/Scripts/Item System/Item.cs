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
    public Sprite Icon { get { return _icon; } }
    public ItemType Type { get { return _type; } }
    public string ItemName { get { return _itemName; } }
    public string Discription { get { return _description; } }
    public int Weight { get { return _weight; } }
}
