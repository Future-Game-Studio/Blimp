using UnityEngine;
using System.Text;



public class ResourceExtraButton : MonoBehaviour, ITIP
{
    public string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("<color=green>Use rope to start docking").Append("</color>");

        return builder.ToString();
    }

    
}
