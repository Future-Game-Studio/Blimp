using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Resource Isle", menuName = "Isle System/Isles/Resource", order = 52)] 
class ResourceIsleLogic : Isle
{
    [SerializeField] private LevelInfo[] _info;

    public LevelInfo[] info { get { return _info; } }

    [System.Serializable]
    public class LevelInfo
    {
        [SerializeField] private ResourceItem _item;
        [SerializeField] private int _resourcesPerHour;
        [SerializeField] private int _maxAmount;

        public Item item { get { return _item; } }
        public int resourcesPerHour { get { return _resourcesPerHour; } }
        public int maxAmount { get { return _maxAmount; } }
    }
    private void Awake()
    {
        _type = IsleType.Resource;
    }
}
