using UnityEngine;
using UnityEngine.UI;

public class ResourceInfo : MonoBehaviour
{
    [SerializeField] private Text _resourceName;
    [SerializeField] private Text _resourceInfo;
    [SerializeField] private Text _resourceCount;
    [SerializeField] private Image _resourceImage;

    private void Awake()
    {
        RectTransform transform = _resourceImage.gameObject.transform as RectTransform;
    }

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
