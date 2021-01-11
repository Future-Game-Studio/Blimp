using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Item", menuName = "Gun System/Gun", order = 53)]
public class Gun : ScriptableObject
{

    public GameObject CannonPrefab;

    public GameObject BulletPrefab;

    public Sprite sprite;

    public float power;
    public string name;
    public float damage;
    public bool ReadyToFight = false;
    public enum GunType { Cannon, MachineGun};
    public GunType gunType;
}
