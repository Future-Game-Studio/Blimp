using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance { private set; get; }

    public Movement _player { private set; get; }
    CameraManager _camera;
    UIController _ui;
    //AudioConroller
    OwnedItems ownedItems;
    IslesManager _islesManager;
    BlimpProperties _blimpProps;
    EnemyManager _enemyManager;

    void Awake()
    {
        if(_instance != null)
            Destroy(this.gameObject);

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        _player = TryToFindPlayer();

    }

    private void Start()
    {

    }


    void Update()
    {

    }


    private Movement TryToFindPlayer()
    {
        try
        {
            return GameObject.FindObjectOfType<Movement>();
        }
        catch
        {
            Debug.LogError("Player not found! Please, add Player to scene for correct gameplay!");
            return null;
        }
    }

    public void StartGame()
    {

    }

    public void StopGame()
    {

    }

    public void ContinueGame()
    {

    }

    public void LoadData()
    {

    }

    public void SaveData()
    {

    }
}
