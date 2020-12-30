using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraIsleSlot : MonoBehaviour
{
    public DefaultIsle Isle { private set; get; }


    public bool IsEmpty => Isle == null;

    public void SetIsle(DefaultIsle isle)
    {
        Isle = isle;
        isle.transform.parent = gameObject.transform;
        isle.transform.localPosition = Vector3.zero;
    }
}
