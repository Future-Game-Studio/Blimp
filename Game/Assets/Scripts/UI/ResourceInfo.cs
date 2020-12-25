using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceInfo : MonoBehaviour
{
    [SerializeField] private Text _resourceName;
    [SerializeField] private Text _resourceInfo;
    [SerializeField] private Text _resourceCount;
    [SerializeField] private TextMeshProUGUI _resourceWeight;
    [SerializeField] private Image _resourceImage;
    [SerializeField] private Slider _collector;
    public Slider Collector { get => _collector; }
    [SerializeField] private TextMeshProUGUI _sliderValue;
    [SerializeField] private TextMeshProUGUI _sliderMaxValue;



    public void ChangeInfo(string name, string info, Sprite sprite)
    {
        _resourceName.text = name;
        _resourceInfo.text = info;
        _resourceImage.sprite = sprite;
    }

    public void ChangeCount(int count, int maxCount, int itemWeight)
    {
        _resourceCount.text = "Amount: " + count + " / " + maxCount;
        _resourceWeight.text = "Weight: " + itemWeight * count;
        if (itemWeight == 0) Debug.LogError("Item weight = 0");
        else count = Mathf.Min(GameManager._instance.Inventory.RemainderWeight / itemWeight, count);
        Debug.Log("Count: " + count + ", max count: " + maxCount + ", weight: " + itemWeight);
        _collector.maxValue = count;
        _sliderMaxValue.text = count.ToString();
    }

    public void ChangeSliderValueText(float value)
    {
        _sliderValue.text = ((int)value).ToString();
    }

}
