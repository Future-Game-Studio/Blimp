using UnityEngine;
using UnityEngine.UI;

public class ResourceInfo : MonoBehaviour
{
    [SerializeField] private Text _resourceName;
    [SerializeField] private Text _resourceInfo;
    [SerializeField] private Text _resourceCount;
    [SerializeField] private Image _resourceImage;

    public void ChangeInfo(string name, string info, Sprite sprite)
    {
        _resourceName.text = name;
        _resourceInfo.text = info;
        _resourceImage.sprite = sprite;
    }

    public void ChangeCount(int count, int maxCount)
    {
        _resourceCount.text = "Amount: " + count + " / " + maxCount;
    }
}
