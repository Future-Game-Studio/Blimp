using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInfo : MonoBehaviour
{
    public string Name;
    public int spawnNum;
    public float Health;
    public float Speed;
    public float shipRotateSpeed;

    public List<GameObject> Weapons = new List<GameObject>();

    int num = 0;
    Rigidbody rb;
    Vector3 m_EulerAngleVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(0, shipRotateSpeed, 0);
        paroll = StartCoroutine(Paroll());
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            StopAllCoroutines();
            EnemyController controller = GetComponentInParent<EnemyController>();
            controller.Dead(spawnNum, Name, transform);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        EnemyController controller = GetComponentInParent<EnemyController>();
        if (other.gameObject == controller.patroolPoints[num])
        {
            if (num != controller.patroolPoints.Length - 1) num++;
            else num = 0;
            point = controller.patroolPoints[num].transform;
        }
        else if (other.tag == "Player")
        {
            StopCoroutine(paroll);
            attack = StartCoroutine(Attack(other.gameObject));
        }
    }

    Transform point;

    Coroutine paroll;
    Coroutine attack;

    private IEnumerator Paroll()
    {
        EnemyController controller = GetComponentInParent<EnemyController>();

        point = controller.patroolPoints[num].transform;
        while (true)
        {
            var lookPos = point.position - transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            rb.velocity = transform.forward * Speed;
            yield return null;
        }
    }

    private IEnumerator Attack(GameObject player)
    {
        //float health = player.GetComponent<> health!!!!!
        point = player.transform;
        while (true)
        {
            var lookPos = point.position - transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            rb.velocity = transform.forward * Speed;
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapons[i].GetComponent<CannonSpot>().Shoot();
            }
            yield return null;
        }
        paroll = StartCoroutine(Paroll());
    }
}
