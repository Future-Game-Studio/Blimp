using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;


public class ResourceExtraButton : MonoBehaviour, ITIP
{
    public ButtonLogic Logic;
    [SerializeField] private Button _button;
    public Button Button { get => _button; }
    [SerializeField] private TextMeshProUGUI _buttonText;

    public delegate void ClickDelegate();
    public ClickDelegate OnClick;

    public void Activate()
    {
        OnClick?.Invoke();
    }

    public string GetTooltipInfoText()
    {
        return Logic.GetTooltipInfoText();
    }

    public void UpdateInfo()
    {
        _buttonText.text = Logic.ButtonText;
    }
}

public abstract class ButtonLogic : ITIP
{
    public string ButtonText { get; protected set; }
    public abstract string GetTooltipInfoText();
}

public class DockButton : ButtonLogic
{
    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<color=green>Use rope to start docking").Append("</color>");

        return builder.ToString();
    }

    public DockButton()
    {
        ButtonText = "Dock Isle";
    }
}

public class UpgradeButton : ButtonLogic
{
    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<color=green>You can upgrade this isle").Append("</color>");

        return builder.ToString();
    }

    public UpgradeButton()
    {
        ButtonText = "Upgrade Isle";
    }
}