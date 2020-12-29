using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICraftItemInfo : UIItemInfo
{
    [SerializeField] private UISliderController _orderSliderController;
    public UISliderController OrderSliderController { get => _orderSliderController; }
    
    [SerializeField] private UISliderController _collectSliderController;
    public UISliderController CollectSliderController { get => _collectSliderController; }

    public void ChangeCollectValue(int count, int maxCount, int itemWeight)
    {
        _itemCount.text = "Amount: " + count + " / " + maxCount;
        _itemWeight.text = "Weight: " + itemWeight * count;

        if (itemWeight == 0) Debug.LogError("Item weight = 0");
        else count = Mathf.Min(GameManager._instance.Inventory.RemainderWeight / itemWeight, count);

        _collectSliderController.ChangeMaxValue(count);
    }

    public void ChangeOrderValue(int maxValue)
    {
        _orderSliderController.ChangeMaxValue(maxValue);
    }

}