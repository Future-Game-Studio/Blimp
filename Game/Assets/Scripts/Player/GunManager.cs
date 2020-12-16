using UnityEngine;
using System.Collections.Generic;

public class GunManager : MonoBehaviour
{
    public float damage = 10f;
    public float range = 1000f;
    public List<Gun> Weapons = new List<Gun>();

    public Transform cannon;
    public ParticleSystem muzzle;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
#if UNITY_EDITOR
        Debug.DrawRay(cannon.position, cannon.forward, Color.yellow);
#endif
        if (Physics.Raycast(cannon.position, cannon.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            target.TakeDamage(damage);
        }
    }
}