using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIResourceItemInfo : UIItemInfo
{
    

    public override void ChangeCount(int count, int maxCount, int itemWeight)
    {
        _itemCount.text = "Amount: " + count + " / " + maxCount;
        _itemWeight.text = "Weight: " + itemWeight * count;
        if (itemWeight == 0) Debug.LogError("Item weight = 0");
        else count = Mathf.Min(GameManager._instance.Inventory.RemainderWeight / itemWeight, count);
        Debug.Log("Count: " + count + ", max count: " + maxCount + ", weight: " + itemWeight);
        _slider.maxValue = count;
        _sliderMaxValue.text = count.ToString();
    }

    public override void ChangeSliderValueText(float value)
    {
        _sliderValue.text = ((int)value).ToString();
    }

}
