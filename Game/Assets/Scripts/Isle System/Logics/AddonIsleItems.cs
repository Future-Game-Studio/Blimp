using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[System.Serializable]
[CreateAssetMenu(fileName = "New AddonIsle Items", menuName = "Inventory System/Other/AddonIsleItems", order = 51)]
public class AddonIsleItems : ScriptableObject
{
    [SerializeField] List<LevelInfo> _info;
    public List<LevelInfo> Info { get => _info; }

    [System.Serializable]
    public class LevelInfo
    {
        [SerializeField] List<CraftableItem> _craftableItems;
        public List<CraftableItem> CraftableItems { get => _craftableItems; }
    }
}
