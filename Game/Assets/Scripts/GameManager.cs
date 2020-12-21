using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance { private set; get; }

    public Movement Player { private set; get; }
    CameraManager _camera;
    UIManager _ui;
    //AudioConroller
    IslesManager _islesManager;
    public BlimpProperties BlimpProperties { private set; get; }

    void Awake()
    {
        if(_instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log(_instance);

        Player = TryToFindPlayer();
        BlimpProperties = TryToFindBlimp();
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
            return FindObjectOfType<Movement>();
        }
        catch
        {
            Debug.LogError("Player not found! Please, add Blimp(Player) to scene for correct gameplay!");
            return null;
        }
    }
    private BlimpProperties TryToFindBlimp()
    {
        try
        {
            return FindObjectOfType<BlimpProperties>();
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
