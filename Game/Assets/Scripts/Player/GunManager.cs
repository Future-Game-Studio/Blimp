using UnityEngine;
using System.Collections.Generic;

public class GunManager : MonoBehaviour
{
    public List<GameObject> Weapons = new List<GameObject>();

    public ParticleSystem muzzle;

    public LayerMask mask;

    void Awake()
    {
        mask = LayerMask.GetMask("Player");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapons[i].GetComponent<CannonSpot>().Shoot();
            }
        }
    }
}