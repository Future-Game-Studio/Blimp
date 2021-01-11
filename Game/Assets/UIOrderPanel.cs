using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIOrderPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _desription;
    [SerializeField] private Image _icon;

    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _minSliderValue;
    [SerializeField] private TextMeshProUGUI _maxSliderValue;
    [SerializeField] private TextMeshProUGUI _currentSliderValue;

    [SerializeField] private RectTransform _componentContent;
    [SerializeField] private RectTransform _craftComponentPrefab;
    private List<UICraftComponentInfo> _craftComponents = new List<UICraftComponentInfo>();

    [SerializeField] private Button _agreeButton;
    public Button AgreeButton { get => _agreeButton; }
    [SerializeField] private Button _disagreeButton;
    public Button DisagreeButton { get => _disagreeButton; }

    public delegate void OrderDelegate(Item item, int amount);
    public OrderDelegate OnOrder;

    private Item _orderItem;
    private int _maxPlayerOrder;

    private void Awake()
    {
        _slider.minValue = 1;

        _slider.onValueChanged.AddListener(UpdateOnSlider);

        _agreeButton.onClick.AddListener(CraftItems);
        _agreeButton.onClick.AddListener(DestroyPanel);
        _disagreeButton.onClick.AddListener(DestroyPanel);
    }

    public void SetSettings(Item item, List<ItemRecipe> components, int maxValue)
    {
        _orderItem = item;
        _label.text = item.Name;
        _desription.text = item.Description;
        _icon.sprite = item.Icon;

        _slider.maxValue = maxValue;
        _maxSliderValue.text = maxValue.ToString();
        _currentSliderValue.text = "1";

        components.ForEach(c =>
        {
            var craft = Instantiate(_craftComponentPrefab.gameObject);
            craft.transform.SetParent(_componentContent, false);
            var component = craft.GetComponent<UICraftComponentInfo>();
            component.Recipe = c;
            _craftComponents.Add(component);
        });

        _maxPlayerOrder = GameManager._instance.Inventory.CanCraftItemsAmount(components);

        if(_slider.maxValue == 1)
        {
            _maxSliderValue.text = "";
            _minSliderValue.text = "";
            _slider.gameObject.SetActive(false);
        }

        Debug.Log(_maxPlayerOrder);
        if (_maxPlayerOrder == 0)
            _agreeButton.interactable = false;

        UpdateComponents(1);
    }
    public void UpdateComponents(float value)
    {
        int sliderValue = (int)value;
        OwnedItems playerItems = GameManager._instance.Inventory.Items;

        _craftComponents.ForEach(c =>
        {
            c.NeedValue = c.Recipe.Amount * Mathf.Max(sliderValue, 1);
            c.HaveValue = playerItems.GetItemAmount(c.Recipe.Item);
            c.UpdateInfo();
        });
    }

    private void UpdateOnSlider(float value)
    {
        var order = (int)value;

        if(order > _maxPlayerOrder)
        {
            _slider.value = order  = _maxPlayerOrder;
        }
        _currentSliderValue.text = order.ToString();

        UpdateComponents(order);
    }

    private void CraftItems()
    {
        OnOrder?.Invoke(_orderItem, (int)_slider.value);
    }

    public void DestroyPanel()
    {
        Destroy(gameObject);
    }
}
