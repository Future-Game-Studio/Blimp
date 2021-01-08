using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICraftPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Button _agreeButton;
    public Button AgreeButton { get => _agreeButton; }
    [SerializeField] private Button _degreeButton;
    public Button DegreeButton { get => _degreeButton; }
    [SerializeField] private RectTransform _componentContent;
    [SerializeField] private RectTransform _craftComponentPrefab;
    private List<UICraftComponentInfo> _craftComponents = new List<UICraftComponentInfo>();


    public void ExitPanel()
    {
        Destroy(gameObject);
    }

    public void SetSettings(string label, List<ItemRecipe> components)
    {
        _label.text = label;


        if (_craftComponents.Count != 0)
        {
            _craftComponents.ForEach(c => Destroy(c.gameObject));
            _craftComponents.Clear();
        }

        components.ForEach(c =>
        {
            var craft = Instantiate(_craftComponentPrefab.gameObject);
            craft.transform.SetParent(_componentContent, false);
            var component = craft.GetComponent<UICraftComponentInfo>();
            component.Recipe = c;
            _craftComponents.Add(component);
        });

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
}
