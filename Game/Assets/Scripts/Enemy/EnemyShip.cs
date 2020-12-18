using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy item", menuName = "Gun System/Enemy", order = 53)]

public class EnemyShip : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string Name;
    public float Health;
}
