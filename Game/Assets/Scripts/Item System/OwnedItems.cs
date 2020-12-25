using System;
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
        ItemSlot slot = _container.Find(s => s.Item == item);
        if (slot != null)
            slot.AddAmount(amount);
        else
            _container.Add(new ItemSlot(item, amount));

        UpdateSummaryWeightAndAmount();

    }

    public void RemoveItem(Item item, int amount)
    {
        ItemSlot slot = _container.Find(s => s.Item == item);
        if (slot == null || slot.Amount < amount)
            Debug.LogError("Slot item\value error");
        slot.RemoveAmount(amount);

        if (slot.Amount == 0)
            Container.Remove(slot);

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
        return Container.Find(s => s.Item == item)?.Amount ?? 0;
    }

    public ItemSlot GetItemSlot(Item item)
    {
        return Container.Find(s => s.Item == item) ?? null;
    }


    public void DebugItems()
    {
        Container.ForEach(s => Debug.Log("Item: " + s.Item + ", count: " + s.Amount));
    }
}

[System.Serializable]
public class ItemSlot : IComparable
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
        if (value < 0)
            Debug.LogError("Item value < 0");
        Amount += value;
        UpdateSummaryWeight();
    }

    public void RemoveAmount(int value)
    {
        if (value > Amount)
            Debug.LogError("negative value > amount");
        Amount -= value;
        UpdateSummaryWeight();
    }

    public void UpdateSummaryWeight()
    {
        Weight = Amount * Item.Weight;
    }

    public int CompareTo(object obj)
    {
        ItemSlot other = obj as ItemSlot;
        if (other == null)
            Debug.LogError("Compare error.");
        return this.Item.CompareTo(other.Item);
    }
}