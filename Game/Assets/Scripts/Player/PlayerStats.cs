using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float MaxHealth;
    private float Health;

    private void Awake()
    {
        Health = MaxHealth;
    }

    public float TakeDamage(float damage)
    {
        Debug.Log("DamageTaken");
        if ((Health - damage) > 0)
            Health -= damage;
        else
            Death();
        return damage;
    }

    public void Death()
    {
        Debug.LogWarning("Player Dead");
    }
}
