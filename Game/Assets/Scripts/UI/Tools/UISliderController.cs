using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISliderController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public Slider Slider { get => _slider; }
    [SerializeField] private TextMeshProUGUI _sliderValue;
    [SerializeField] private TextMeshProUGUI _sliderMaxValue;

    public void UpdateValue(float value)
    {
        _sliderValue.text = ((int)value).ToString();
    }

    public void ChangeMaxValue(float value)
    {
        _slider.maxValue = value;
        _sliderMaxValue.text = ((int)value).ToString();
    }
}
