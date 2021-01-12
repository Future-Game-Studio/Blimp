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
        patroll = StartCoroutine(Patroll());
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
    }

    Transform point;

    public Coroutine patroll;
    public Coroutine attack;
    public Coroutine shoot;
    public IEnumerator Patroll()
    {
        EnemyController controller = GetComponentInParent<EnemyController>();

        point = controller.patroolPoints[num].transform;
        while (true)
        {
            var lookPos = point.position - transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            rb.velocity = transform.forward * Speed;
            yield return null;
        }
    }

    public IEnumerator Attack(Transform playerPoint)
    {
        //float health = player.GetComponent<> health!!!!!
        shoot = StartCoroutine(Shoot());
        while (true)
        {
            //Debug.Log("player pos " + playerPoint.position);
            var lookPos = playerPoint.position - transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            rb.velocity = transform.forward * Speed;
            yield return null;

        }
    }

    public IEnumerator Shoot()
    {
        while (true)
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapons[i].GetComponent<CannonSpot>().Shoot(CannonSpot.ShootMode.Player);
                yield return new WaitForSeconds(1);
            }
    }
}
