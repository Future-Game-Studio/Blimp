using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportShip : MonoBehaviour
{
    public GameObject ship;
    public Transform target;
    public Vector3 EnterCoords;
    public GameObject Button;

    public float telSpeed = 2;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "House")
        {
            Vector3 destination = other.gameObject.GetComponent<DefaultIsle>()._position;

            Debug.Log("triggered house");
            ship.GetComponent<Movement>().enabled = false;
            Button.SetActive(true);
            StartCoroutine(MovementToDest(destination));
        }
    }

    public void ExitHouse()
    {
        StartCoroutine(MovementToDest(EnterCoords));
        ship.GetComponent<Movement>().enabled = true;
        Button.SetActive(false);
    }

    IEnumerator MovementToDest(Vector3 _dest_mov)
    {
        EnterCoords = transform.position;
        while (transform.position != _dest_mov)
        {
            Vector3 direction = target.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _dest_mov, Time.deltaTime * telSpeed);
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, telSpeed * Time.deltaTime);
            Debug.Log("moving");
            yield return null;

        }
    }

}
