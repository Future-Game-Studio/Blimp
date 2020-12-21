using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Debug.Log("object dead");

    }
}
