using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public CannonSpot.ShootMode mode;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("played " + mode);
        if (other.tag == "Enemy" && mode == CannonSpot.ShootMode.Enemy)
        {
            other.GetComponent<ShipInfo>().TakeDamage(damage);
            Debug.Log("Enemy damaged");
        }
        if (other.tag == "Player" && mode == CannonSpot.ShootMode.Player)
        {
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            Debug.Log("Player damaged");
        }
        Destroy(this);
    }
}
