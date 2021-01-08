using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICraftItemInfo : UIItemInfo
{
    [SerializeField] private UISliderController _orderSliderController;
    public UISliderController OrderSliderController { get => _orderSliderController; }

    [SerializeField] private UISliderController _collectSliderController;
    public UISliderController CollectSliderController { get => _collectSliderController; }

    [SerializeField] private RectTransform _craftComponentsPanel;
    [SerializeField] private RectTransform _craftComponentPrefab;
    private List<UICraftComponentInfo> _craftComponents = new List<UICraftComponentInfo>();

    private void Start()
    {
        OrderSliderController.OnUpdateValue += UpdateComponents;
    }


    public void ChangeCollectValue(int count, int maxCount, int itemWeight)
    {
        _itemCount.text = "Amount: " + count + " / " + maxCount;
        _itemWeight.text = "Weight: " + itemWeight * count;

        if (itemWeight == 0) Debug.LogError("Item weight = 0");
        else count = Mathf.Min(GameManager._instance.Inventory.RemainderWeight / itemWeight, count);

        _collectSliderController.ChangeMaxValue(count);
    }

    public void UpdateComponents(float value)
    {
        int sliderValue = (int)value;
        OwnedItems playerItems = GameManager._instance.Inventory.Items;

        _craftComponents.ForEach(c =>
        {
            c.NeedValue = c.Recipe.Amount * Mathf.Max(sliderValue, 1);
            c.HaveValue = playerItems.GetItemAmount(c.Recipe.Item);
            c.UpdateInfo();
        });
    }

    public void ChangeOrderValue(int maxValue)
    {
        _orderSliderController.ChangeMaxValue(maxValue);

        UpdateComponents(_orderSliderController.Slider.value);
    }

    public void SpawnCraftComponents(List<ItemRecipe> components)
    {
        if (_craftComponents.Count != 0)
        {
            _craftComponents.ForEach(c => Destroy(c.gameObject));
            _craftComponents.Clear();
        }

        components.ForEach(c =>
        {
            var craft = Instantiate(_craftComponentPrefab.gameObject);
            craft.transform.SetParent(_craftComponentsPanel, false);
            var component = craft.GetComponent<UICraftComponentInfo>();
            component.Recipe = c;
            _craftComponents.Add(component);
        });
    }

}