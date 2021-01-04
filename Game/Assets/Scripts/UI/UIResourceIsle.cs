using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourceIsle : UIController
{
    private ResourcesIsle _isle;
    [SerializeField] private RectTransform _buttonPrefab;
    [SerializeField] private RectTransform _contentList;
    [SerializeField] private UIResourceItemInfo _resourceinfo;
    private List<ResourceButton> _buttons = new List<ResourceButton>();
    [SerializeField] private Scrollbar _scroll;
    [SerializeField] private GameObject _collectResourceButton;
    [SerializeField] private GameObject _extraButton;

    private Item _currentItemInfo;
    private int _level;
    private int _maxLevel;

    public override void UpdateAll()
    {
        _isle = UIManager._instance.LastActiveIsle as ResourcesIsle;
        if (_isle == null)
            Debug.LogError("Isle type error");

        _isle.OnRefresh += UpdateByItem;

        

        UpdateStaticInfo();
        UpdateDynamicInfo();
        _currentItemInfo = _isle.Logic.Info[0].Item;
        UpdateResourceInfo(_currentItemInfo);
        UpdateExtraButton();

    }


    private void UpdateStaticInfo()
    {
        if (_buttons != null)
            _buttons.ForEach(b => Destroy(b.gameObject));
        _buttons.Clear();

        _level = _isle.Level;
        _maxLevel = _isle.Logic.Info.Count;

        for(int i = 0; i < _maxLevel; i++)
        {
            var buttonObj = Instantiate(_buttonPrefab.gameObject);
            buttonObj.transform.SetParent(_contentList, false);
            var button = buttonObj.GetComponent<ResourceButton>();
            _buttons.Add(button);
            if(i >= _level)
            {
                button.Button.interactable = false;
                button.ClearCount();
            }
            _buttons[i].ChangeItem(_isle.Logic.Info[i].Item);
            _buttons[i].OnClick += UpdateResourceInfo;
        }
    }


    private void UpdateByItem(Item item)
    {
        ItemSlot slot = _isle.Items.GetItemSlot(item);
        if (slot == null)
            Debug.LogError("Item find error!");

        int count = slot.Amount;
        int maxCount = _isle.Logic.Info.Find(i => i.Item == item).MaxAmount;
        var button = _buttons.Find(b => b.Item == item);
        button.ChangeCount(count, maxCount);
        button.ChangeProgress(_isle.RefreshedItems[item]);

        if(item == _currentItemInfo)
        {
            ItemSlot currentSlot = _isle.Items.Container.Find(s => s.Item == _currentItemInfo);
            _resourceinfo.ChangeCount(currentSlot.Amount, _isle.Logic.Info.Find(s => s.Item == _currentItemInfo).MaxAmount, currentSlot.Item.Weight);
        }
    }

    private void UpdateResourceInfo(Item item)
    {
        _currentItemInfo = item;
        _resourceinfo.ChangeInfo(_currentItemInfo.Name, _currentItemInfo.Description, _currentItemInfo.Icon);
        ItemSlot currentSlot = _isle.Items.Container.Find(s => s.Item == _currentItemInfo);
        _resourceinfo.ChangeCount(currentSlot.Amount, _isle.Logic.Info.Find(s => s.Item == _currentItemInfo).MaxAmount, currentSlot.Item.Weight);
    }
    private void UpdateDynamicInfo()
    {
        for (int i = 0; i < _level; i++)
        {
            int count = _isle.Items.Container[i].Amount;
            int maxCount = _isle.Logic.Info[i].MaxAmount;
            _buttons[i].ChangeCount(count, maxCount);
            _buttons[i].ChangeProgress(_isle.RefreshedItems[_isle.Logic.Info[i].Item]);
        }


    }

    private void OnDisable()
    {
        _isle.OnRefresh -= UpdateByItem;
        _scroll.value = 1;
    }

    public void CollectResource()
    {
        Inventory inventory = GameManager._instance.Inventory;
        int remainderWeight = inventory.RemainderWeight;
        ItemSlot slot = _isle.Items.Container.Find(s => s.Item == _currentItemInfo);
        int value = (int)_resourceinfo.SliderController.Slider.value;
        int needWeight = _currentItemInfo.Weight * value;

        if (remainderWeight < needWeight)
            Debug.LogError("Remainder weight < need weight");

        inventory.Add(slot.Item, value);
        slot.RemoveAmount(value);
        UpdateResourceInfo(_currentItemInfo);
        UpdateByItem(_currentItemInfo);
    }

    private void UpdateExtraButton()
    {
        var button = _extraButton.GetComponent<ResourceExtraButton>();
        switch (_isle.Mode)
        {
            case DockMode.Inside:
                button.Logic = new UpgradeButton();
                break;
            case DockMode.Outside:
                button.Logic = new DockButton();
                var ropeItem = GameManager._instance.IsleManager.RopeItem;
                if (GameManager._instance.Inventory.ItemIsExist(ropeItem) == false)
                    button.Button.interactable = false;
                break;
        }
        button.UpdateInfo();
    }
}
