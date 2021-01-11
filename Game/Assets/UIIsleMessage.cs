using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIIsleMessage : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _type;
    [SerializeField] private TextMeshProUGUI _description;
    
    private UIType _activateUI;
    private DynamicIsle _isle;

    public void SetSettings(UIType uiType, DynamicIsle isle, string name, string description, string type)
    {
        _activateUI = uiType;
        _isle = isle;

        _label.text = name;
        _description.text = description;
        _type.text = type;
    }

    public void ChangeUI()
    {
        UIManager._instance.SwitchIsleUI(_activateUI, _isle);
    }
}
