using UnityEngine;
using System.Text;



public class ResourceExtraButton : MonoBehaviour, ITIP
{
    public ButtonLogic Logic;

    public string GetTooltipInfoText()
    {
        return Logic.GetTooltipInfoText();
    }

    public void UpdateInfo()
    {
        
    }
}

public abstract class ButtonLogic : ITIP
{
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
}

public class UpgradeButton : ButtonLogic
{
    public override string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<color=green>You can upgrade this isle").Append("</color>");

        return builder.ToString();
    }
}