using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyShip enemyInfo;



    //movement will be made later now only health and dying
    void Start()
    {
        Vector3 spawn = new Vector3(4, 0, 0);
        GameObject currentEntity = Instantiate(enemyInfo.prefab, spawn, Quaternion.identity);
        currentEntity.name = enemyInfo.Name;

    }

}
