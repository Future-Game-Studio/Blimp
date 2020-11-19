using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerDistance
{
    Near,
    Average,
    Far
}

public class ResourcesIsle : MonoBehaviour
{
    [SerializeField] ResourceIsleLogic _logic;
    [SerializeField] private OwnedItems _items;
    [SerializeField] private Item _itemType;
    [SerializeField] private int _maxAmount;

    private PlayerDistance _distanceMode = PlayerDistance.Near;
    private int _distanceMultyplier = 1;

    const float _avarageDis = 150;
    const float _farDis = 500;
    private void Start()
    {
        _items = new OwnedItems();
        StartCoroutine(Refreshing());
    }

    private void Update()
    {
    }

    //ref coroutine
    private IEnumerator Refreshing()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f * _distanceMultyplier);

            if(_maxAmount > _items._summaryAmount)
            {
                int currentMaxAmount = _maxAmount - _items._summaryAmount;
                int currentAmount = 1 * _distanceMultyplier;
                currentAmount = Mathf.Min(currentMaxAmount, currentAmount);
                _items.AddItem(_itemType, currentAmount);
            }

            Debug.Log(UpdatePlayerDistance());
            Debug.Log(_items._summaryAmount);

        }
    }

    private float UpdatePlayerDistance()
    {
        Vector3 _playerPos = GameManager._instance._player.gameObject.transform.position;
        Vector3 _islePos = gameObject.transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow((_playerPos.x - _islePos.x), 2) + Mathf.Pow((_playerPos.y - _islePos.y), 2));

        _distanceMode = PlayerDistance.Near;
        if (distance > _farDis)
            _distanceMode = PlayerDistance.Far;
        else if (distance > _avarageDis)
            _distanceMode = PlayerDistance.Average;

        switch (_distanceMode)
        {
            case PlayerDistance.Near:
                _distanceMultyplier = 1;
                break;
            case PlayerDistance.Average:
                _distanceMultyplier = 10;
                break;
            case PlayerDistance.Far:
                _distanceMultyplier = 30;
                break;
        }

        return distance;
    }
}
