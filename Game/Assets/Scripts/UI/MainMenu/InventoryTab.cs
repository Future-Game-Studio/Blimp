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
    [SerializeField] private UIInventoryItemInfo _itemInfo;
    private ItemSlot _currentSlot;
    private ItemFilterType _currentFilter;
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
            slot.OnClick += ShowItemInfo;
        }
    }

    public void ShowAll()
    {
        ShowTypeItems(ItemFilterType.All);
    }

    public void ShowTypeItems(ItemFilterType type, bool setNewCurrentSlot = true)
    {
        ClearSlots();

        _weight.text = _inventory.CurrentWeight.ToString() + " / " + _inventory.MaxWeight.ToString();
        List<ItemSlot> container = _inventory.Items.Container;
        if (type != ItemFilterType.All)
            container = container.FindAll(s => s.Item.FilterType == type);
        container.Sort();
        GenerateSlots(container);

        if(container.Count == 0)
            _currentSlot = null;
        else if(setNewCurrentSlot)
            _currentSlot = container[0];
        ShowItemInfo(_currentSlot);

        _currentFilter = type;
    }

    private void ShowItemInfo(ItemSlot itemSlot)
    {

        if (itemSlot != null)
        {
            if (_itemInfo.gameObject.activeInHierarchy == false)
                _itemInfo.gameObject.SetActive(true);

            Item item = itemSlot.Item;
            _itemInfo.ChangeInfo(item.Name, item.Description, item.Icon);
            _itemInfo.ChangeCount(itemSlot.Amount, itemSlot.Amount, itemSlot.Item.Weight);
            _currentSlot = itemSlot;
        }
        else
            _itemInfo.gameObject.SetActive(false);
    }

    public void RemoveItem()
    {
        if (_currentSlot == null)
            Debug.LogError("Item does not exist");
        int amount = (int)_itemInfo.SliderController.Slider.value;
        Item item = _currentSlot.Item;
        _inventory.Remove(item, amount);
        _currentSlot = _inventory.Items.Container.Find(s => s.Item == item);
        ShowTypeItems(_currentFilter, false);
    }

}
