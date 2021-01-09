using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRemovePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _minSliderValue;
    [SerializeField] private TextMeshProUGUI _maxSliderValue;
    [SerializeField] private TextMeshProUGUI _currentSliderValue;
    [SerializeField] private TextMeshProUGUI _weight;

    [SerializeField] private Button _removeButton;
    public Button RemoveButton { get => _removeButton; }
    [SerializeField] private Button _backButton;
    public Button BackButton { get => _backButton; }

    private ItemSlot _removeItem;
    public delegate void DeleteDelegate(Item item, int amount);
    public DeleteDelegate OnDeleteClick;

    private void Awake()
    {
        _slider.minValue = 1;

        _backButton.onClick.AddListener(DestroyPanel);
        _removeButton.onClick.AddListener(DeleteItems);
        _removeButton.onClick.AddListener(DestroyPanel);

        _slider.onValueChanged.AddListener(OnSliderUpdate);
    }

    public void SetItemSlot(ItemSlot _slot)
    {
        _removeItem = _slot;
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        Item item = _removeItem.Item;
        int amount = _removeItem.Amount;
        _label.text = item.Name;
        _icon.sprite = item.Icon;
        _description.text = item.Description;

        _maxSliderValue.text = amount.ToString();
        _slider.maxValue = amount;

        _weight.text = (item.Weight * (int)_slider.value).ToString();

        if (_removeItem.Amount == 1)
        {
            _slider.value = 1;
            _slider.gameObject.SetActive(false);
            _minSliderValue.text = "";
            _maxSliderValue.text = "";
            _currentSliderValue.text = "1";
        }
    }

    private void OnSliderUpdate(float value)
    {
        var amount = (int)value;
        _currentSliderValue.text = amount.ToString();
        _weight.text = (_removeItem.Item.Weight * amount).ToString();
    }

    private void DeleteItems()
    {
        OnDeleteClick?.Invoke(_removeItem.Item, (int)_slider.value);
    }

    private void DestroyPanel()
    {
        Destroy(gameObject);
    }
}
