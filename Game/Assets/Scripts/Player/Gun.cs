using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damege = 10f;
    public float range = 1000f;

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
        if (Physics.Raycast(cannon.position, cannon.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
