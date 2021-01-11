using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExtraIsle : UIController
{
    private ExtraIsle _isle;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private RectTransform _DockPanel;
    [SerializeField] private RectTransform _ChooseTypePanel;
    [SerializeField] private RectTransform _buttonPrefab;
    [SerializeField] private RectTransform _contentList;
    [SerializeField] private UICraftItemInfo _iteminfo;
    private List<ResourceButton> _buttons = new List<ResourceButton>();
    [SerializeField] private Scrollbar _scroll;
    [SerializeField] private Button _orderButton;
    [SerializeField] private Button _collectButton;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private RectTransform _upgradePanel;
    [SerializeField] private RectTransform _orderPanel;
    [SerializeField] private RectTransform _collectPanel;

    private CraftableItem _currentItemInfo;
    private GameObject _addonPanel;
    private int _level = 1;
    private int _maxLevel;

    public override void UpdateAll()
    {
        _isle = UIManager._instance.LastActiveIsle as ExtraIsle;
        if (_isle == null)
            Debug.LogError("Isle type error");

        if (_isle.Type == IsleType.Empty)
        {
            //rewrite
            _mainUI.SetActive(false);
            //
            if (_isle.Mode == DockMode.Outside)
            {
                _addonPanel = Instantiate(_DockPanel.gameObject, gameObject.transform as RectTransform);
                DockPanel dock = _addonPanel.GetComponent<DockPanel>();
                var ropeItem = GameManager._instance.ItemManager.Rope;
                if (GameManager._instance.Inventory.ItemIsExist(ropeItem) == false)
                    dock.DockButton.interactable = false;
                else
                    dock.DockButton.onClick.AddListener(DockIsle);
                dock.ExitButton.onClick.AddListener(ExitAddonPanel);
            }
            else if (_isle.Mode == DockMode.Inside)
            {
                _addonPanel = Instantiate(_ChooseTypePanel.gameObject, gameObject.transform as RectTransform);
                ChooseTypePanel choose = _addonPanel.GetComponent<ChooseTypePanel>();
                choose.CraftButton.onClick.AddListener(ChooseCraftIsle);
                choose.FabricButton.onClick.AddListener(ChooseFabricIsle);
                choose.ExitButton.onClick.AddListener(ExitAddonPanel);
            }
            else if(_isle.Mode == DockMode.Docking)
            {
                UIManager._instance.SwitchUI(UIType.HUD);
            }
            return;
        }
        else
        {
            //
            if (_mainUI.activeInHierarchy == false)
                _mainUI.SetActive(true);
            //
        }

        _isle.OnDoTask += UpdateByItem;

        UpdateCrafts();
        UpdateCurrentInfo(_isle.Items.Info[0].CraftableItems[0]);

        UpdateUpgradeButton();
    }

    private void UpdateCrafts()
    {
        if (_buttons != null)
            _buttons.ForEach(b => Destroy(b.gameObject));
        _buttons.Clear();


        _level = _isle.Level;
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

    private void UpdateByItem(CraftableItem item, float progress, bool dontUpdateProgressIfCurrent = false)
    {
        if (item == null)
            Debug.LogError("Item type error");

        int maxOrder = item.MaxOrder;
        int doneCount = _isle.DoneTasks.GetItemAmount(item);
        ResourceButton button = _buttons.Find(b => b.Item == item);
        button.ChangeCount(doneCount, maxOrder);

        if (item == _currentItemInfo)
        {
            UpdateCurrentInfo(item);
        }
        if (!(dontUpdateProgressIfCurrent && (_isle.Tasks.Count == 0 || _isle.Tasks[0].Item == item)))
            button.ChangeProgress(progress);
    }

    private void UpdateCurrentInfo(Item getitem)
    {
        CraftableItem item = getitem as CraftableItem;
        if (item == null)
            Debug.LogError("Item type error");

        _currentItemInfo = item;
        _iteminfo.ChangeInfo(_currentItemInfo.Name, _currentItemInfo.Description, _currentItemInfo.Icon);
        ItemSlot currentSlot = _isle.DoneTasks.GetItemSlot(item);
        int amount = 0;
        if (currentSlot != null)
            amount = currentSlot.Amount;
        _iteminfo.ChangeCollectValue(amount, item.MaxOrder, item.Weight);

        if (amount == 0)
            _collectButton.interactable = false;
        else if (_collectButton.interactable == false)
            _collectButton.interactable = true;

        if (_currentItemInfo.MaxOrder - amount == 0)
            _orderButton.interactable = false;
        else if (_orderButton.interactable == false)
            _orderButton.interactable = true;

    }

    public void CollectItems()
    {
        var panelObj = Instantiate(_collectPanel.gameObject, transform as RectTransform);
        var panel = panelObj.GetComponent<UICollectPanel>();
        var slot = _isle.DoneTasks.GetItemSlot(_currentItemInfo);

        panel.SetItemSlot(slot);
        panel.OnCollectClick += CollectItems;

    }

    public void CollectItems(Item item, int amount)
    {
        Inventory inventory = GameManager._instance.Inventory;
        int remainderWeight = inventory.RemainderWeight;
        ItemSlot slot = _isle.DoneTasks.Container.Find(s => s.Item == _currentItemInfo);
        int needWeight = item.Weight * amount;

        if (remainderWeight < needWeight)
            Debug.LogError("Remainder weight < need weight");

        inventory.Add(slot.Item, amount);
        slot.RemoveAmount(amount);
        UpdateCurrentInfo(item);
        UpdateByItem(item as CraftableItem, 0, true);
    }



    public void OrderItems()
    {
        var panel = Instantiate(_orderPanel, transform as RectTransform).GetComponent<UIOrderPanel>();
        var components = _currentItemInfo.Recipe;

        ItemSlot currentSlot = _isle.DoneTasks.GetItemSlot(_currentItemInfo);
        int maxOrderValue = _currentItemInfo.MaxOrder;
        if (currentSlot != null)
            maxOrderValue -= currentSlot.Amount;

        panel.SetSettings(_currentItemInfo, components, maxOrderValue);
        panel.OnOrder += OrderItems;
    }

    private void OrderItems(Item itemI, int amount)
    {
        var item = itemI as CraftableItem;
        if (_currentItemInfo != item)
            Debug.LogError("item order error");

        _isle.AddTask(item, amount);

        GameManager._instance.Inventory.RemoveItems(item.Recipe, amount);
        UpdateCurrentInfo(item);
        UpdateByItem(item, 0, true);
    }
    ////////////////////FULL REWRITE//////////////////////////////

    private void DockIsle()
    {
        GameManager._instance.Inventory.Remove(GameManager._instance.ItemManager.Rope, 1);
        _isle.StartDock();
        ExitAddonPanel();
    }
    private void ChooseCraftIsle()
    {
        _isle.ChangeIsleType(IsleType.Craft);
        UpdateUI();
    }

    private void ChooseFabricIsle()
    {
        _isle.ChangeIsleType(IsleType.Fabric);
        UpdateUI();
    }

    private void UpdateUI()
    {
        ExitAddonPanel();
        _mainUI.SetActive(true);
        UpdateCrafts();
        UpdateCurrentInfo(_isle.Items.Info[0].CraftableItems[0]);
        UpdateUpgradeButton();
        _isle.OnDoTask += UpdateByItem;
    }

    private void ExitAddonPanel()
    {
        if (_addonPanel == null)
            Debug.LogError("Ui addon panel exist error");

        Destroy(_addonPanel.gameObject);
        _addonPanel = null;

        if (GameManager._instance.Mode == GameMode.InMainIsle)
            UIManager._instance.SwitchUI(UIType.MainIsle);
        else UIManager._instance.SwitchUI(UIType.HUD);
    }

    ///////////////////////////////////////////////

    private void UpdateUpgradeButton()
    {
        if (_isle.Items.Info.Count == _isle.Level)
            _upgradeButton.interactable = false;
        else
        {
            _upgradeButton.onClick.RemoveAllListeners();
            _upgradeButton.onClick.AddListener(InstantiateUpgradePanel);
        }
    }

    private void InstantiateUpgradePanel()
    {
        GameObject panelObj = Instantiate(_upgradePanel.gameObject, transform as RectTransform);
        UICraftPanel panel = panelObj.GetComponent<UICraftPanel>();
        panel.DisagreeButton.onClick.AddListener(panel.ExitPanel);

        int lvl = _isle.Level;
        List<ItemRecipe> components = _isle.Items.Info[lvl].Recipe;

        panel.SetSettings("Upgrade isle", components);
        if (GameManager._instance.Inventory.HasRecipeItems(components) == false)
            panel.AgreeButton.interactable = false;
        else
        {
            panel.AgreeButton.onClick.AddListener(IncreaseIsleLevel);
            panel.AgreeButton.onClick.AddListener(panel.ExitPanel);
        }
    }

    private void IncreaseIsleLevel()
    {
        var components = _isle.GetLvlUpItems();
        var playerInventory = GameManager._instance.Inventory;
        if (playerInventory.HasRecipeItems(components) == false)
            Debug.LogError("Items exist error");

        playerInventory.RemoveItems(components);
        _isle.IncreaseLevel();
        UpdateAll();
    }

    private void OnDisable()
    {
        if (_isle != null)
            _isle.OnDoTask -= UpdateByItem;
        _scroll.value = 1;
    }

    public void ExitUI()
    {
        if (GameManager._instance.Mode == GameMode.InMainIsle)
            UIManager._instance.SwitchUI(UIType.MainIsle);
        else UIManager._instance.SwitchUI(UIType.HUD);
    }
}
