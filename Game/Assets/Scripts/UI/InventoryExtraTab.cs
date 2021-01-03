using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryExtraTab : MonoBehaviour
{
    [SerializeField] private InventoryTab _inventoryTab;
    [SerializeField] private ItemFilterType _type;

    public void ActivateType()
    {
        _inventoryTab.ShowTypeItems(_type);
    }
}
