﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Item", menuName = "Gun System/Gun", order = 53)]
public class Gun : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string Name;
    public float Damage;
}