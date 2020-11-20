using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Item", menuName = "Inventory System/Items/Resource", order = 51)]
public class ResourceItem : Item
{
    private void Awake()
    {
        _type = ItemType.Resource;
    }


}
