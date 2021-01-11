using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Item _rope;
    public Item Rope { get => _rope; }
    [SerializeField] private Item _uksus;
    public Item Uksus { get => _uksus; }
}
