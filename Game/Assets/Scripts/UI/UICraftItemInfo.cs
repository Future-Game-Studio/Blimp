using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICraftItemInfo : UIItemInfo
{
    public void ChangeCollectValue(int count, int maxCount, int itemWeight)
    {
        _itemCount.text = "Amount: " + count + " / " + maxCount;
        _itemWeight.text = "Weight: " + itemWeight * count;
    }
}