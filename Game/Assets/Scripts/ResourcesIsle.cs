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
    [SerializeField] private int _level;
    public int level { get { return _level; } }
    [SerializeField] private OwnedItems _items;
    [SerializeField] ResourceIsleLogic _logic;
    [SerializeField] private List<float> _refreshPerSecond;
    [SerializeField] private List<float> _refreshedAmount;

    //[SerializeField] private Item _itemType;
    //[SerializeField] private int _maxAmount;

    #region distance check
    private PlayerDistance _distanceMode = PlayerDistance.Near;
    private int _distanceMultyplier = 1;
    const float _avarageDis = 150;
    const float _farDis = 500;

    #endregion

    private void Start()
    {
        _items = new OwnedItems();
        _refreshPerSecond = new List<float>();
        _refreshedAmount = new List<float>();
        //load owned item info
        //load isle mode(inside, outside)

        UpdateRefreshInfo();
        StartCoroutine(Refreshing());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            IncreaseLevel();
        }
    }

    private void IncreaseLevel()
    {
        if(_level == _logic.info.Length)
            Debug.LogError("Isle level the same as max!");
        else
        {
            _level++;
            UpdateRefreshInfo();
        }
    }

    private void UpdateRefreshInfo()
    {
        _refreshPerSecond.Clear();
        for (int i = 0; i < _level; i++)
        {
            _refreshPerSecond.Add((float)_logic.info[i].resourcesPerHour / 3600);
        }

        while(_refreshedAmount.Count < _level)
        {
            _refreshedAmount.Add(0.0f);
        }
    }

    //ref coroutine
    private IEnumerator Refreshing()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f * _distanceMultyplier);

            for(int i = 0; i < _level; i++)
            {
                _refreshedAmount[i] += _refreshPerSecond[i] * _distanceMultyplier;

                Item currentItem = _logic.info[i].item;
                int currentAmount = _items.GetItemAmount(currentItem);
                int maxRefreshedAmount = _logic.info[i].maxAmount - currentAmount;
                if (_refreshedAmount[i] > maxRefreshedAmount)
                    _refreshedAmount[i] = Mathf.Min(_refreshedAmount[i], maxRefreshedAmount);

                if(_refreshedAmount[i] >= 1)
                {
                    int addAmount = (int)_refreshedAmount[i];
                    _items.AddItem(currentItem, addAmount);
                    _refreshedAmount[i] -= addAmount;
                }
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
