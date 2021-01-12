using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ShipInfo shipInfo = GetComponentInParent<ShipInfo>();
        if (other.tag == "Player")
        {
            //Debug.Log(other.name + " trigger");
            shipInfo.StopCoroutine(shipInfo.patroll);
            shipInfo.attack = shipInfo.StartCoroutine(shipInfo.Attack(other.GetComponent<Transform>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ShipInfo shipInfo = GetComponentInParent<ShipInfo>();
        if (other.tag == "Player")
        {
            shipInfo.StopCoroutine(shipInfo.shoot);
            shipInfo.StopCoroutine(shipInfo.attack);
            shipInfo.patroll = shipInfo.StartCoroutine(shipInfo.Patroll());
        }
    }
}
