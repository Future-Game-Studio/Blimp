using UnityEngine;

public class UIResourceItemInfo : UIItemInfo
{

    public void ChangeCount(int count, int maxCount, int itemWeight)
    {
        _itemCount.text = "Amount: " + count + " / " + maxCount;
        _itemWeight.text = "Weight: " + itemWeight * count;

    }

}
