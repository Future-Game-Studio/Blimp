using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _itemName;
    [SerializeField] protected TextMeshProUGUI _itemInfo;
    [SerializeField] protected TextMeshProUGUI _itemCount;
    [SerializeField] protected TextMeshProUGUI _itemWeight;
    [SerializeField] protected Image _itemImage;
    public virtual void ChangeInfo(string name, string info, Sprite sprite)
    {
        _itemName.text = name;
        _itemInfo.text = info;
        _itemImage.sprite = sprite;
    }

    public virtual void ChangeAmount(int count, int weight)
    {
        _itemCount.text = "Amount: " + count;
        _itemWeight.text = "Weight: " + weight;
    }
}