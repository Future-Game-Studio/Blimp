using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Owned Items", menuName = "Inventory System/Other/OwnedItem", order = 51)]
public class OwnedItems : ScriptableObject
{
    [SerializeField] private List<ItemSlot> _container;
    public List<ItemSlot> Container { get { return _container; } }
    public int SummaryWeight { get; private set; }
    public int SummaryAmount { get; private set; }

    void Awake()
    {
        _container = new List<ItemSlot>();
    }
    public void AddItem(Item item, int amount)
    {
        bool has = false;

        for(int i = 0; i < _container.Count; i++)
        {
            if (_container[i].Item == item)
            {
                _container[i].AddAmount(amount);
                has = true;
                break;
            }
        }

        if (!has)
            _container.Add(new ItemSlot(item, amount));

        UpdateSummaryWeightAndAmount();
    }

    private void UpdateSummaryWeightAndAmount()
    {
        SummaryWeight = 0;
        SummaryAmount = 0;
        for (int i = 0; i < Container.Count; i++)
        {
            SummaryAmount += Container[i].Amount;
            SummaryWeight += Container[i].Weight;
        }
    }

    public int GetItemAmount(Item item)
    {
        for(int i = 0; i < Container.Count; i++)
        {
            if (Container[i].Item == item)
                return Container[i].Amount;
        }
        return 0;
    }

}

[System.Serializable]
public class ItemSlot
{
    public Item Item { get; }
    public int Amount { get; private set; }
    public int Weight { private set; get; }

    public ItemSlot(Item item, int amount)
    {
        Item = item;
        Amount = amount;

        UpdateSummaryWeight();
    }

    public void AddAmount(int value)
    {
        Amount += value;
        UpdateSummaryWeight();
    }

    public void UpdateSummaryWeight()
    {
        Weight = Amount * Item.Weight;
    }
}