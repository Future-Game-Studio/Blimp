using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class UICraftComponentInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _componentInfo;

    public CraftableItem.ItemRecipe Recipe;
    public int HaveValue;
    public int NeedValue;
    public void UpdateInfo()
    {
        StringBuilder b = new StringBuilder();

        b.Append(Recipe.Item.Name).Append(" - ");
        b.Append("<color=green>").Append(HaveValue).Append("</color>").Append(" / ");
        b.Append(NeedValue);

        _componentInfo.text = b.ToString();
    }

    
}
