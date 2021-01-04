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
        Isle.transform.localScale = Isle.StartScale;
        Isle.transform.parent = gameObject.transform;
        Isle.transform.localPosition = Vector3.zero;
        Isle.transform.rotation = Quaternion.identity;
    }
}
