using UnityEngine;
using System.Collections.Generic;

public class GunManager : MonoBehaviour
{
    public List<GameObject> Weapons = new List<GameObject>();

    public ParticleSystem muzzle;

    public LayerMask mask = LayerMask.GetMask("Enemy");


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
        Debug.DrawRay(Weapons[0].transform.position, Weapons[0].transform.forward, Color.yellow);
#endif



        if (Physics.Raycast(Weapons[0].transform.position, Weapons[0].transform.forward, out hit, Weapons[0].GetComponent<CannonSpot>().gun.range, mask))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            target.TakeDamage(Weapons[0].GetComponent<CannonSpot>().gun.damage);
        }

    }
}