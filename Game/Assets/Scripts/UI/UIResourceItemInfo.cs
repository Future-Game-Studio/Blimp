using UnityEngine;

public class UIResourceItemInfo : UIItemInfo
{
    [SerializeField] protected UISliderController _sliderController;
    public UISliderController SliderController { get => _sliderController; }

    public void ChangeCount(int count, int maxCount, int itemWeight)
    {
        _itemCount.text = "Amount: " + count + " / " + maxCount;
        _itemWeight.text = "Weight: " + itemWeight * count;

        if (itemWeight == 0) Debug.LogError("Item weight = 0");
        else count = Mathf.Min(GameManager._instance.Inventory.RemainderWeight / itemWeight, count);

        Debug.Log("Count: " + count + ", max count: " + maxCount + ", weight: " + itemWeight);
        _sliderController.ChangeMaxValue(count);
    }

}
