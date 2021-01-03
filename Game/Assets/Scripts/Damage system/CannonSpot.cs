using System.Collections;
using UnityEngine;

public class CannonSpot : GunManager
{
    private enum LookMode { Enemy, StartPos };
    public Gun gun;
    protected GameObject ChildGameObject;
    public float power;


    public void Start()
    {
    }


    Coroutine Look;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Transform enemy = other.GetComponent<Transform>();
            Look = StartCoroutine(LookAtEnemy(enemy, LookMode.Enemy));
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            StopCoroutine(Look);
            ChildGameObject.transform.rotation = transform.rotation;
            StartCoroutine(LookAtEnemy(transform, LookMode.StartPos));
        }
    }

    public void Shoot()
    {
        ChildGameObject = this.transform.GetChild(0).gameObject;

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, ChildGameObject.transform.forward, Color.yellow);
#endif
        GameObject bullet;
        bullet = Instantiate(gun.BulletPrefab, ChildGameObject.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().damage = gun.damage;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = ChildGameObject.transform.forward * gun.power;
        StartCoroutine(DestroyAfter(bullet, 4.0f));
    }

    private IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }

    private IEnumerator LookAtEnemy(Transform tr, LookMode lookMode)
    {
        ChildGameObject = this.transform.GetChild(0).gameObject;
        while (true && lookMode == LookMode.Enemy)
        {
            var lookPos = tr.position - ChildGameObject.transform.position;
            lookPos.y = 1f;
            var rotation = Quaternion.LookRotation(lookPos);
            ChildGameObject.transform.rotation = Quaternion.Slerp(ChildGameObject.transform.rotation, rotation, Time.deltaTime);
            yield return null;
        }
        while (true && lookMode == LookMode.StartPos)
        {
            var lookPos = tr.position - ChildGameObject.transform.position;
            lookPos.y = 1f;
            lookPos.x = 0f;
            lookPos.z = 0f;
            var rotation = Quaternion.LookRotation(lookPos);
            ChildGameObject.transform.rotation = Quaternion.Slerp(ChildGameObject.transform.rotation, rotation, Time.deltaTime);

            if (ChildGameObject.transform.rotation == transform.rotation)
            {
                break;
            }
            yield return null;
        }
    }
}
