using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerDistance
{
    Near = 1,
    Average = 10,
    Far = 30
}

public class ResourcesIsle : DefaultIsle
{
    #region refresh item
    [SerializeField] private int _level;
    public int Level { get => _level; }
    public OwnedItems Items { get; private set; }
    [SerializeField] private ResourceIsleLogic _logic;
    public ResourceIsleLogic Logic { get => _logic; }
    public Dictionary<Item, float> RefreshedItems { private set; get; }//item and float amount(adding every second(10/30) with small value)
    #endregion

    #region distance check
    private PlayerDistance _distanceMode = PlayerDistance.Near;
    const float _avarageDis = 150;
    const float _farDis = 500;
    #endregion

    private void Awake()
    {
        Type = IsleType.Resource;
    }

    private void Start()
    {
        Items = ScriptableObject.CreateInstance<OwnedItems>();
        foreach(ResourceIsleLogic.LevelInfo info in _logic.Info)
        {
            Items.AddItem(info.Item, 0);
        }
        RefreshedItems = new Dictionary<Item, float>();
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
        if (_level == _logic.Info.Length)
            Debug.LogError("Isle level the same as max!");
        else
        {
            _level++;
            UpdateRefreshInfo();
        }
    }

    private void UpdateRefreshInfo()
    {
        if (RefreshedItems.Count < _level)
        {
            for (int i = RefreshedItems.Count; i < _level; i++)
            {
                RefreshedItems.Add(_logic.Info[i].Item, 0);
            }
        }
    }

    //ref coroutine
    private IEnumerator Refreshing()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f * (int)_distanceMode);

            for (int i = 0; i < _level; i++)
            {
                Item item = _logic.Info[i].Item;


                RefreshedItems[item] += _logic.Info[i].ResourcePerSecond * (int)_distanceMode;
                int currentAmount = Items.GetItemAmount(item);
                int maxRefreshedAmount = _logic.Info[i].MaxAmount - currentAmount;
                if (RefreshedItems[item] > maxRefreshedAmount)
                    RefreshedItems[item] = Mathf.Min(RefreshedItems[item], maxRefreshedAmount);

                if (RefreshedItems[item] >= 1)
                {
                    int addAmount = (int)RefreshedItems[item];
                    Items.AddItem(item, addAmount);
                    RefreshedItems[item] -= addAmount;
                }
            }


            float distance = UpdatePlayerDistance();

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
                break;
            case PlayerDistance.Average:
                break;
            case PlayerDistance.Far:
                break;
        }

        return distance;
    }

    void OnMouseDown()
    {
        UIManager._instance.SwitchIsleUI(UIType.ResourceIsle, this);
        Debug.Log("Ckick");
    }
}
