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
    [SerializeField] protected Slider _slider;
    public Slider Slider { get => _slider; }
    [SerializeField] protected TextMeshProUGUI _sliderValue;
    [SerializeField] protected TextMeshProUGUI _sliderMaxValue;

    public virtual void ChangeInfo(string name, string info, Sprite sprite)
    {
        _itemName.text = name;
        _itemInfo.text = info;
        _itemImage.sprite = sprite;
    }

    public virtual void ChangeCount(int count, int maxCount, int itemWeight)
    {
        _itemCount.text = "Count: " + count.ToString();
        _slider.maxValue = maxCount;
        _sliderMaxValue.text = maxCount.ToString();
        _itemWeight.text = "Weight: " + (itemWeight * count).ToString();
    }

    public virtual void ChangeSliderValueText(float value)
    {
        _sliderValue.text = ((int)value).ToString();
    }

    
}
