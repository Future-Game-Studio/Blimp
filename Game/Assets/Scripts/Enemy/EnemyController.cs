using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private EnemyShip[] _enemyShipsInfo;
    private EnemyShip enemy;
    [SerializeField]
    public GameObject[] patroolPoints;
    private List<GameObject> _enemyShips = new List<GameObject>();

    GameObject enemyGO;
    Vector3 m_EulerAngleVelocity;

    private void Awake()
    {
        EnemySpawn();
    }

    private GameObject EnemySpawn()
    {
        enemy = _enemyShipsInfo[0];
        enemyGO = Instantiate(enemy.prefab, transform.position, transform.rotation, transform);
        ShipInfo info = enemyGO.GetComponent<ShipInfo>();
        _enemyShips.Add(enemyGO.gameObject);
        info.spawnNum = _enemyShips.Count;
        info.Health = enemy.Health;
        info.Name = enemy.Name;
        info.Speed = enemy.Speed;
        info.shipRotateSpeed = enemy.shipRotateSpeed;
        return enemyGO;
    }

    private GameObject EnemySpawn(int num)
    {
        enemy = _enemyShipsInfo[0];
        GameObject _enemy = _enemyShips[num - 1];
        _enemy.GetComponent<ShipInfo>().Health = enemy.Health;
        _enemy.GetComponent<Transform>().position = transform.position;
        return enemyGO;
    }

    public void Dead(int num, string _name, Transform gameObject)
    {
        EnemySpawn(num);
        Debug.Log(_name + num + "dead");
    }

    public void Dead(int num, string _name)
    {
        EnemySpawn();
        Debug.Log(_name + num + "dead");
        //анімація вибуху + інектів
    }
}
