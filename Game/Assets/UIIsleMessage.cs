using UnityEngine;
using UnityEngine.UI;

public class UIIsleMessage : MonoBehaviour
{
    [SerializeField] private Button _button;
    private UIType _activateUI;
    private DefaultIsle _isle;

    public void SetSettings(UIType type, DefaultIsle isle)
    {
        _activateUI = type;
        _isle = isle;
    }

    public void ChangeUI()
    {
        UIManager._instance.SwitchIsleUI(_activateUI, _isle);
    }
}
