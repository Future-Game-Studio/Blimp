using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        GameObject Enemy;
        ShipInfo EnemyInfo;
        Movement PlayerInfo;
        if (other.TryGetComponent<ShipInfo>(out EnemyInfo))
        {
            Enemy = other.gameObject;
            Enemy.GetComponent<ShipInfo>().TakeDamage(damage);
        }
        else if(other.TryGetComponent<Movement>(out PlayerInfo))
        {
            Enemy = other.gameObject;
            Enemy.GetComponent<Movement>().TakeDamage(damage);
        }
    }
}
