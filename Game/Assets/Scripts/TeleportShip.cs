using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportShip : MonoBehaviour
{
    public GameObject ship;
    
    public float telSpeed = 2;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "House")
        {
            Vector3 destination = other.gameObject.GetComponent<Isles>().ShipCoords;

            Debug.Log("triggered house");
            ship.GetComponent<movement>().enabled = false;
            
            StartCoroutine(MovementToDest(destination));
        }
    }

    IEnumerator MovementToDest(Vector3 _dest)
    {
        while (transform.position != _dest)
        {
            transform.position = Vector3.MoveTowards(transform.position, _dest, Time.deltaTime * telSpeed);
            if(transform.position == _dest)
                yield break;
            Debug.Log("moving");
            yield return null;

        }
    }

}
