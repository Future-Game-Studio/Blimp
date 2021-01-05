using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        GameObject Enemy;
        if (other.tag == "Enemy")
        {
            Enemy = other.gameObject;
            Enemy.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }
}
