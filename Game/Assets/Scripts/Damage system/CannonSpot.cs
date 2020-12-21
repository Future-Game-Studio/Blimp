using UnityEngine;

public class CannonSpot : GunManager
{
    public Gun gun;

    public void OnTriggerEnter(Collider other)
    {
#if UNITY_EDITOR
        Debug.Log("Collision with enemy");
#endif


        if(other.tag == "Enemy")
        {
            
        }
    }

}
