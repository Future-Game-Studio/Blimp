using UnityEngine;
using System.Collections.Generic;

public class GunManager : MonoBehaviour
{
    public List<GameObject> Weapons = new List<GameObject>();

    public ParticleSystem muzzle;

    public LayerMask mask = LayerMask.GetMask("Enemy");
    protected delegate void CannonDelegate(int damage,GameObject position);
    protected CannonDelegate cannonDelegate;

    void Awake()
    {
        cannonDelegate = Shoot;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

        }
    }

    protected void Shoot(int damage, GameObject position)
    {
        RaycastHit hit;
#if UNITY_EDITOR
        Debug.DrawRay(Weapons[0].transform.position, Weapons[0].transform.forward, Color.yellow);
#endif

        if (Physics.Raycast(position.transform.position, position.transform.forward, out hit, mask))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            target.TakeDamage(damage);
        }

    }
}