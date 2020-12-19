using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraIsleSlot : MonoBehaviour
{
    [SerializeField] private GameObject _isle;


    public bool IsEmpty()
    {
        return _isle != null;
    }
}
