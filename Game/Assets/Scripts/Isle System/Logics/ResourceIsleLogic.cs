using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Resource Isle", menuName = "Isle System/Isles/Resource", order = 52)] 
class ResourceIsleLogic : Isle
{
    [SerializeField] private LevelInfo[] _info;
    public LevelInfo[] Info { get { return _info; } }

    [System.Serializable]
    public class LevelInfo
    {
        [SerializeField] private ResourceItem _item;
        [SerializeField] private int _resourcesPerHour;
        [SerializeField] private int _maxAmount;

        public Item Item { get { return _item; } }
        public int ResourcesPerHour { get { return _resourcesPerHour; } }
        public int MaxAmount { get { return _maxAmount; } }

        public float ResourcePerSecond { get { return (float)_resourcesPerHour / 3600; } }
    }
    private void Awake()
    {
        _type = IsleType.Resource;
    }
}
