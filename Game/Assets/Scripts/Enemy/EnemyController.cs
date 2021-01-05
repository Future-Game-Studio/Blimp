using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyShip[] _enemyShips;
    private EnemyShip enemy;
    private float health;
    private string name;
    [SerializeField]
    private GameObject[] patroolPoints;

    int num = 0;
    Rigidbody rb;
    GameObject enemyGO;
    Vector3 m_EulerAngleVelocity;

    private void Awake()
    {
        enemy = _enemyShips[0];
        enemyGO = Instantiate(enemy.prefab, transform.position, transform.rotation, transform);
        rb = GetComponent<Rigidbody>();

        health = enemy.Health;
        name = enemy.Name;
        m_EulerAngleVelocity = new Vector3(0, enemy.shipRotateSpeed, 0);
        paroll = StartCoroutine(Paroll());
    }

    Coroutine paroll;
    Coroutine attack;


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
        Debug.Log(name + " dead");
        //анімація вибуху + інектів
    }

    Transform point;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == patroolPoints[num])
        {
            if (num != patroolPoints.Length - 1) num++;
            else num = 0;
            point = patroolPoints[num].transform;
        }
    }

    private IEnumerator Paroll()
    {
        point = patroolPoints[num].transform;
        while (true)
        {
            var lookPos = point.position - transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            rb.velocity = transform.forward * enemy.Speed;
            yield return null;
        }
    }

    private IEnumerator Attack()
    {
        yield return null;
    }
}
