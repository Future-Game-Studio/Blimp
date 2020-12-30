using UnityEngine;
using System;
public enum ItemType
{
    Recovering,
    Craftable,
    Other
}

public enum ItemFilterType
{
    Resource,
    Processed,
    Craft,
    Equipment,
    Other,
    All
}

public abstract class Item : ScriptableObject, IComparable
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected ItemType _type;
    [SerializeField] protected ItemFilterType _filterType;
    [SerializeField] protected string _name;
    [TextArea(15, 20)] [SerializeField] protected string _description;
    [SerializeField] protected int _weight;
    [SerializeField] protected int _level;
    public Sprite Icon { get => _icon; }
    public ItemType Type { get => _type; }
    public ItemFilterType FilterType { get => _filterType; }
    public string Name { get => _name; }
    public abstract string ColouredName { get; }
    public string Description { get => _description;  }
    public int Weight { get => _weight; }
    public int Level { get => _level; }

    public int CompareTo(object obj)
    {
        Item other = obj as Item;
        if (other == null)
            Debug.LogError("Compare error.");

        if (this.Type == other.Type)
            return this.Level.CompareTo(other.Level);

        return this.Type.CompareTo(other.Type);
    }
}
