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
    [SerializeField] private UIItemInfo _itemInfo;
    [SerializeField] private RectTransform _removePanel;
    private ItemSlot _currentSlot;
    private ItemFilterType _currentFilter;

    private void Start()
    {
        if (_inventory == null)
            _inventory = GameManager._instance.Inventory;

        ShowAll();
    }
    public override void UpdateAll()
    {

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

        if (container.Count == 0)
            _currentSlot = null;
        else if (setNewCurrentSlot)
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
            _itemInfo.ChangeAmount(itemSlot.Amount, itemSlot.Weight);
            _currentSlot = itemSlot;
        }
        else
            _itemInfo.gameObject.SetActive(false);
    }

    public void RemoveItem()
    {
        var panel = Instantiate(_removePanel, transform as RectTransform).GetComponent<UIRemovePanel>();
        panel.SetItemSlot(_currentSlot);

        panel.OnDeleteClick += RemoveItem;
    }

    public void RemoveItem(Item item, int amount)
    {
        if (_currentSlot.Item != item)
            Debug.LogError("Item does not exist");

        _inventory.Remove(item, amount);
        _currentSlot = _inventory.Items.Container.Find(s => s.Item == item);
        ShowTypeItems(_currentFilter, false);
    }
}
