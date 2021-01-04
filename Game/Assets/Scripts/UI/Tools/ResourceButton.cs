using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceButton : SelectableButton
{
    public Item Item { get; private set; }
    
    [SerializeField] private RadialBar _progressBar;
    [SerializeField] private int _buttonNum;
    public int ButtonNum { get => _buttonNum; }
    [SerializeField] private TextMeshProUGUI _resourceName;
    [SerializeField] private TextMeshProUGUI _resourceCount;

    public delegate void ItemThrow(Item item);
    public ItemThrow OnClick;

    public void ChangeItem(Item item)
    {
        this.Item = item; 
        _resourceName.text = item.Name;
    }

    public void ChangeCount(int count, int maxCount)
    {
        _resourceCount.text = count + " / " + maxCount;
    }

    public void ClearCount()
    {
        _resourceCount.text = "";
    }

    public void ChangeProgress(float value)
    {
        _progressBar.ChangeValue(value);
    }

    public override void Activate()
    {
        OnClick?.Invoke(Item);
    }

    public override void Deactivate()
    {
        //Deactivate
    }
}
