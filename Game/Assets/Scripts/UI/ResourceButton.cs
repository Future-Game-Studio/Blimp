using UnityEngine;
using UnityEngine.UI;

public class ResourceButton : Button
{
    [SerializeField] private RadialBar _progressBar;
    [SerializeField] private int _buttonNum;
    public int ButtonNum { get => _buttonNum; }
    [SerializeField] private Text _resourceName;
    [SerializeField] private Text _resourceCount;

    public void ChangeName(string name)
    {
        _resourceName.text = name;
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
}
