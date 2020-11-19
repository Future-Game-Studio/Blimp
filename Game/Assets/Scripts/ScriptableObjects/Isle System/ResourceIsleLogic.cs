using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Resource Isle", menuName = "Isle System/Isles/Resource", order = 52)]
public class ResourceIsleLogic : Isle
{
    private int _level;
    [SerializeField] private LevelInfo[] _info;

    [System.Serializable]
    class LevelInfo
    {
        public ResourceItem _item;
        public int _resourcesPerHour;
    }
    private void Awake()
    {
        _type = IsleType.Resource;
    }
}
