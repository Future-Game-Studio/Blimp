using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Owned Items", menuName = "Inventory System/OwnedItem", order = 52)]
public class OwnedItems : ScriptableObject
{
    public List<ItemSlot> _container = new List<ItemSlot>();
    public int _summaryWeight { private set; get; }
    public int _summaryAmount { private set; get; }
    public void AddItem(Item item, int amount)
    {
        bool has = false;

        for(int i = 0; i < _container.Count; i++)
        {
            if (_container[i]._item == item)
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
        _summaryWeight = 0;
        _summaryAmount = 0;
        for (int i = 0; i < _container.Count; i++)
        {
            _summaryAmount += _container[i]._amount;
            _summaryWeight += _container[i]._summaryWeight;
        }
    }


}

[System.Serializable]
public class ItemSlot
{
    public Item _item { private set; get; }
    public int _amount { private set; get; }
    public int _summaryWeight { private set; get; }

    public ItemSlot(Item item, int amount)
    {
        _item = item;
        _amount = amount;

        UpdateSummaryWeight();
    }

    public void AddAmount(int value)
    {
        _amount += value;
        UpdateSummaryWeight();
    }

    public void UpdateSummaryWeight()
    {
        _summaryWeight = _amount * _item._weight;
    }
}