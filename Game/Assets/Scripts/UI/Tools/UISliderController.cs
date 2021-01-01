using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISliderController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public Slider Slider { get => _slider; }
    [SerializeField] private TextMeshProUGUI _sliderValue;
    [SerializeField] private TextMeshProUGUI _sliderMaxValue;

    public delegate void UpdateValueDelegate(float value);
    public UpdateValueDelegate OnUpdateValue;

    public void UpdateValue(float value)
    {
        _sliderValue.text = ((int)value).ToString();
        OnUpdateValue?.Invoke(value);
    }

    public void ChangeMaxValue(float value)
    {
        if (value == 0)
        {
            _sliderValue.text = _sliderMaxValue.text = "";
            _slider.gameObject.SetActive(false);
        }
        else
        {
            if (_slider.gameObject.activeInHierarchy == false)
                _slider.gameObject.SetActive(true);
            _slider.maxValue = value;
            _sliderMaxValue.text = ((int)value).ToString();
        }
    }
}
