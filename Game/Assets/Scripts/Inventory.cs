using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public OwnedItems Items { get; private set; }
    public int MaxWeight { get; private set; }
    public int CurrentWeight { get => Items.SummaryWeight; }
    public int RemainderWeight { get => MaxWeight - Items.SummaryWeight; }
    public Inventory()
    {
        MaxWeight = 125;
        if (Items == null)
            Items = ScriptableObject.CreateInstance<OwnedItems>();
    }

    public void Add(Item item, int count)
    {
        Items.AddItem(item, count);
    }

    public void Remove(Item item, int count)
    {
        Items.RemoveItem(item, count);
    }

    public void RemoveItems(List<ItemRecipe> list, int amount = 1)
    {
        list.ForEach(r =>
        {
            Items.RemoveItem(r.Item, r.Amount * amount);
        });
    }

    public bool ItemIsExist(Item item)
    {
        return Items.GetItemAmount(item) != 0;
    }

    public bool HasRecipeItems(List<ItemRecipe> list)
    {
        bool has = true;
        list.ForEach(r =>
        {
            if (Items.GetItemAmount(r.Item) < r.Amount)
                has = false;
        });
        return has;
    }

    public int CanTakeItemAmount(Item item)
    {
        return RemainderWeight / item.Weight;
    }

    public int CanCraftItemAmount(ItemRecipe recipe)
    {
        return Items.GetItemAmount(recipe.Item) / recipe.Amount;
    }

    public int CanCraftItemsAmount(List<ItemRecipe> list)
    {
        int amount = CanCraftItemAmount(list[0]);
        list.ForEach(r =>
        {
            int currentAmount = CanCraftItemAmount(r);
            amount = Mathf.Min(amount, currentAmount);
        });

        return amount;
    }

    public int GetUksusAmount()
    {
        Item uksus = GameManager._instance.ItemManager.Uksus;
        return Items.GetItemAmount(uksus);
    }

    public int GetRopeAmount()
    {
        Item rope = GameManager._instance.ItemManager.Rope;
        return Items.GetItemAmount(rope);
    }
}
