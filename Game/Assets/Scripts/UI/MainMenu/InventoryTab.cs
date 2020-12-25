using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryTab : MainMenuTab
{
    private Inventory _inventory;
    List<InventorySlot> _slots = new List<InventorySlot>();
    [SerializeField] private RectTransform _contentGrid;
    [SerializeField] private RectTransform _slotPrefab;
    [SerializeField] private TextMeshProUGUI _weight;
    public override void UpdateAll()
    {
        if (_inventory == null)
            _inventory = GameManager._instance.Inventory;

        ShowAll();
    }

    private void ClearSlots()
    {
        if (_slots.Count != 0)
        {
            _slots.ForEach(s => Destroy(s.gameObject));
            _slots.Clear();
        }
    }

    private void GenerateSlots(List<ItemSlot> container)
    {
        int count = container.Count;
        for (int i = 0; i < count; i++)
        {
            var slotObj = Instantiate(_slotPrefab.gameObject);
            slotObj.transform.SetParent(_contentGrid, false);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.SetInfo(container[i]);
            _slots.Add(slot);
        }
    }

    public void ShowAll()
    {
        ClearSlots();

        List<ItemSlot> container = _inventory.Items.Container;
        container.Sort();
        GenerateSlots(container);

        _weight.text = _inventory.CurrentWeight.ToString() + " / " + _inventory.MaxWeight.ToString();
    }

    public void ShowTypeItems(ItemType type)
    {
        ClearSlots();


        List<ItemSlot> container = _inventory.Items.Container;
        container = container.FindAll(s => s.Item.Type == type);
        container.Sort();
        GenerateSlots(container);
    }

}
