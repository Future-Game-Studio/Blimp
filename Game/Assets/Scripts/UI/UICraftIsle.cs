using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftIsle : UIController
{
    private CraftIsle _isle;
    [SerializeField] private RectTransform _buttonPrefab;
    [SerializeField] private RectTransform _contentList;
    [SerializeField] private UICraftItemInfo _iteminfo;
    private List<ResourceButton> _buttons = new List<ResourceButton>();
    [SerializeField] private Scrollbar _scroll;
    [SerializeField] private GameObject _orderResourceButton;
    [SerializeField] private GameObject _collectResourceButton;

    private CraftableItem _currentItemInfo;
    private int _level;
    private int _maxLevel;

    public override void UpdateAll()
    {
        _isle = UIManager._instance.LastActiveIsle as CraftIsle;
        if (_isle == null)
            Debug.LogError("Isle type error");

        _isle.OnDoTask += UpdateByItem;

        UpdateCrafts();



    }

    private void UpdateCrafts()
    {
        if (_buttons != null)
            _buttons.ForEach(b => Destroy(b.gameObject));
        _buttons.Clear();


        _level = _isle.Level;
        //
        _level = 1;
        //
        _maxLevel = _isle.Items.Info.Count;

        for (int i = 0; i < _maxLevel; i++)
        {
            var l = _isle.Items.Info[i];
            l.CraftableItems.ForEach(item =>
            {
                var buttonObj = Instantiate(_buttonPrefab.gameObject);
                buttonObj.transform.SetParent(_contentList, false);
                var button = buttonObj.GetComponent<ResourceButton>();
                _buttons.Add(button);

                if (i >= _level)
                {
                    button.Button.interactable = false;
                    button.ClearCount();
                }
                button.ChangeItem(item);
                UpdateByItem(item, 1);
                button.OnClick += UpdateCurrentInfo;
            });
        }
        //
        _isle.Tasks.ForEach(t =>
        {
            _buttons.Find(b => b.Item == t.Item).ChangeProgress(0);
        });
        //
        UpdateByItem(_isle.Items.Info[0].CraftableItems[0], 1);
    }

    private void UpdateByItem(CraftableItem item, float progress)
    {
        _currentItemInfo = item;
        if (item == null)
            Debug.LogError("Item type error");

        int maxOrder = item.MaxOrder;
        int doneCount = _isle.DoneTasks.GetItemAmount(item);
        ResourceButton button = _buttons.Find(b => b.Item == item);
        button.ChangeCount(doneCount, maxOrder);
        button.ChangeProgress(progress);

        if (item == _currentItemInfo)
        {
            UpdateCurrentInfo(item);
        }
    }

    private void UpdateCurrentInfo(Item getitem)
    {
        CraftableItem item = getitem as CraftableItem;
        if (item == null)
            Debug.LogError("Item type error");

        _currentItemInfo = item;
        _iteminfo.ChangeInfo(_currentItemInfo.Name, _currentItemInfo.Description, _currentItemInfo.Icon);
        ItemSlot currentSlot = _isle.DoneTasks.GetItemSlot(item);

        int maxOrderValue = item.MaxOrder;

        if (currentSlot == null)
            _iteminfo.ChangeCollectValue(0, item.MaxOrder, item.Weight);
        else
        {
            maxOrderValue -= currentSlot.Amount;
            _iteminfo.ChangeCollectValue(currentSlot.Amount, item.MaxOrder, item.Weight);
        }

        var needItems = item.Recipe;
        maxOrderValue = CanCraftAmount(needItems, maxOrderValue);

        CraftIsle.CraftTask task = _isle.Tasks.Find(t => t.Item == item);

        if (task != null)
            maxOrderValue -= task.Amount;

        maxOrderValue = Mathf.Max(maxOrderValue, 0);
        _iteminfo.ChangeOrderValue(maxOrderValue);
    }

    private int CanCraftAmount(List<CraftableItem.ItemRecipe> needItems, int maxValue)
    {
        Inventory inventory = GameManager._instance.Inventory;
        needItems.ForEach(recipe =>
        {
            Item item = recipe.Item;
            int needAmount = recipe.Amount;

            int haveAmount = inventory.Items.GetItemAmount(item);

            int canCraftAmount = haveAmount / needAmount;

            maxValue = Mathf.Min(maxValue, canCraftAmount);
        });

        return maxValue;
    }

    public void CollectItems()
    {
        Inventory inventory = GameManager._instance.Inventory;
        int remainderWeight = inventory.RemainderWeight;
        ItemSlot slot = _isle.DoneTasks.Container.Find(s => s.Item == _currentItemInfo);
        int value = (int)_iteminfo.CollectSliderController.Slider.value;
        int needWeight = _currentItemInfo.Weight * value;

        if (remainderWeight < needWeight)
            Debug.LogError("Remainder weight < need weight");

        inventory.Add(slot.Item, value);
        slot.RemoveAmount(value);
        UpdateCurrentInfo(_currentItemInfo);
        UpdateByItem(_currentItemInfo, 1);
    }

    public void OrderItems()
    {
        int value = (int)_iteminfo.OrderSliderController.Slider.value;
        _isle.AddTask(_currentItemInfo, value);

        //remove items from inventory
        Inventory inventory = GameManager._instance.Inventory;
        var needItems = _currentItemInfo.Recipe;
        needItems.ForEach(recipe =>
        {
            inventory.Remove(recipe.Item, recipe.Amount * value);
        });

        UpdateCurrentInfo(_currentItemInfo);
        UpdateByItem(_currentItemInfo, 1);
    }

    private void OnDisable()
    {
        _isle.OnDoTask -= UpdateByItem;
        _scroll.value = 1;
    }
}
