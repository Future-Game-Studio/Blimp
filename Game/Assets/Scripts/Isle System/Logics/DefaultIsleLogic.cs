using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Default Isle", menuName = "Isle System/Isles/Default", order = 52)]
public class DefaultIsleLogic : Isle
{


    private void Awake()
    {
        _type = IsleType.Default;
    }
}
