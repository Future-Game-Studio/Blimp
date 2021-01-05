using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyShip[] _enemyShips;
    private EnemyShip enemy;
    public float health;

    private void Awake()
    {
        enemy = _enemyShips[0];
        Instantiate(enemy.prefab, transform.position, Quaternion.identity, transform);
        health = enemy.Health;
    }

    public void Update()
    {

    }

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
        Debug.Log("EnemyController dead");
    }
}
