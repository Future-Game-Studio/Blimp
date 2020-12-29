using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _itemName;
    [SerializeField] protected TextMeshProUGUI _itemInfo;
    [SerializeField] protected TextMeshProUGUI _itemCount;
    [SerializeField] protected TextMeshProUGUI _itemWeight;
    [SerializeField] protected Image _itemImage;
    public virtual void ChangeInfo(string name, string info, Sprite sprite)
    {
        _itemName.text = name;
        _itemInfo.text = info;
        _itemImage.sprite = sprite;
    }

}

public class UIInventoryItemInfo : UIItemInfo
{
    [SerializeField] protected UISliderController _sliderController;
    public UISliderController SliderController { get => _sliderController; }

    public virtual void ChangeCount(int count, int maxCount, int itemWeight)
    {
        _itemCount.text = "Count: " + count.ToString();
        _sliderController.ChangeMaxValue(count);
        _itemWeight.text = "Weight: " + (itemWeight * count).ToString();
    }
}
