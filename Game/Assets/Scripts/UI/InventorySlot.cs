using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class InventorySlot : MonoBehaviour, ITIP
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _amount;
    ItemSlot _slot;

    public string GetTooltipInfoText()
    {
        StringBuilder builder = new StringBuilder();
        Item item = _slot.Item;
        builder.Append("<color=green>").Append(item.Name).Append("</color>").AppendLine();
        builder.Append("<color=blue>").Append(item.Type.ToString()).Append("</color>").AppendLine();
        builder.Append("<color=purple>").Append(item.Description).Append("</color>").AppendLine();
        builder.Append("<color=white>").Append(_slot.Weight).Append("</color>").AppendLine();

        return builder.ToString();
    }

    public void SetInfo(ItemSlot slot)
    {
        _slot = slot;
        _image.sprite = _slot.Item.Icon;
        _amount.text = _slot.Amount.ToString();
    }
}
