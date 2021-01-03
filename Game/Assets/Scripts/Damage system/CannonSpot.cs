using System.Collections;
using UnityEngine;

public class CannonSpot : GunManager
{
    public Gun gun;
    protected GameObject ChildGameObject;
    public float power;

    public void Start()
    {
        GameObject bullet;

    }
    public void Shoot()
    {
        ChildGameObject = this.transform.GetChild(0).gameObject;

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, ChildGameObject.transform.forward, Color.yellow);
#endif
        GameObject bullet;
        bullet = Instantiate(gun.BulletPrefab, this.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().damage = gun.damage;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = this.transform.forward * gun.power;
        StartCoroutine(DestroyAfter(bullet, 4.0f));
    }

    private IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }


}
