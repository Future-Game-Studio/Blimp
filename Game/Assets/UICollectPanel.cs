using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICollectPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _minSliderValue;
    [SerializeField] private TextMeshProUGUI _maxSliderValue;
    [SerializeField] private TextMeshProUGUI _currentSliderValue;
    [SerializeField] private TextMeshProUGUI _weight;

    [SerializeField] private Button _collectButton;
    public Button CollectButton { get => _collectButton; }
    [SerializeField] private Button _backButton;
    public Button BackButton { get => _backButton; }

    private ItemSlot _collectItem;
    public delegate void CollectDelegate(Item item, int amount);
    public CollectDelegate OnCollectClick;

    private int _playerMaxAmount;
    private int _playerRemainderWeight;
    private void Awake()
    {
        _slider.minValue = 1;

        _backButton.onClick.AddListener(DestroyPanel);
        _collectButton.onClick.AddListener(CollectItem);
        _collectButton.onClick.AddListener(DestroyPanel);
        _slider.onValueChanged.AddListener(UpdateOnSlider);
    }

    public void SetItemSlot(ItemSlot _slot)
    {
        _collectItem = _slot;
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        Item item = _collectItem.Item;
        int amount = _collectItem.Amount;
        _label.text = item.Name;
        _icon.sprite = item.Icon;
        _description.text = item.Description;

        _maxSliderValue.text = amount.ToString();
        _slider.maxValue = amount;

        var inventory = GameManager._instance.Inventory;

        _playerMaxAmount = inventory.CanTakeItemAmount(item);
        _playerRemainderWeight = inventory.RemainderWeight;

        _weight.text = _playerRemainderWeight + " / " + item.Weight * (int)_slider.value;

        if (_playerMaxAmount == 0)
            _collectButton.interactable = false;
        else if(_collectItem.Amount == 1)
        {
            _slider.value = 1;
            _slider.gameObject.SetActive(false);
            _minSliderValue.text = "";
            _maxSliderValue.text = "";
            _currentSliderValue.text = "1";
        }
    }

    private void UpdateOnSlider(float value)
    {
        int amount = (int)value;

        if (amount > _playerMaxAmount)
            _slider.value = amount = _playerMaxAmount;

        _currentSliderValue.text = amount.ToString();
        _weight.text = _playerRemainderWeight + " / " + _collectItem.Item.Weight * amount;
    }

    private void CollectItem()
    {
        OnCollectClick?.Invoke(_collectItem.Item, (int)_slider.value);
    }

    private void DestroyPanel()
    {
        Destroy(gameObject);
    }


}
