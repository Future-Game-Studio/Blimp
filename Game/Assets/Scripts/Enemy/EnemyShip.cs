using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyController item", menuName = "Enemy System/New enemy", order = 53)]

public class EnemyShip : ScriptableObject
{
    public enum EnemyType { defaultType, attack };
    public EnemyType enemyType;
    public string Name;
    public float Health;
    public float Speed;
    public float shipRotateSpeed;

    public GameObject prefab;
    public Sprite sprite;
}
