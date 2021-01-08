using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



[CreateAssetMenu(fileName = "New Resource Isle", menuName = "Isle System/Isles/Resource", order = 52)] 
public class ResourceIsleItems : Isle
{
    [SerializeField] private List<LevelInfo> _info;
    public List<LevelInfo> Info { get => _info; }

    [System.Serializable]
    public class LevelInfo
    {
        [SerializeField] private ResourceItem _item;
        [SerializeField] private int _resourcesPerHour;
        [SerializeField] private int _maxAmount;
        [SerializeField] private List<ItemRecipe> _recipe;
        public Item Item { get => _item; }
        public int ResourcesPerHour { get => _resourcesPerHour; }
        public int MaxAmount { get => _maxAmount; }

        public float ResourcePerSecond { get => (float)_resourcesPerHour / 3600; }
        public List<ItemRecipe> Recipe { get => _recipe; }
    }
    private void Awake()
    {
        _type = IsleType.Resource;
    }
}
