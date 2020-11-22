using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyIsle : DefaultIsle
{
    [SerializeField] private AddonIsleItems _items;

    private void Start()
    {
        _items = AddonIsleLogic._instance.CraftItems;
    }
}
