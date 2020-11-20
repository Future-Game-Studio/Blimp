using UnityEngine;

public enum ItemType
{
    Resource,
    Craftable,
    Equipment
}

public abstract class Item : ScriptableObject
{
    public Sprite _icon;
    public ItemType _type;
    public string _itemName;
    [TextArea(15, 20)]
    public string _description;
    public int _weight;
}
