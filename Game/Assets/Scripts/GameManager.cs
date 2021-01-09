using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance { private set; get; }

    public Movement Player { private set; get; }
    public CameraManager Camera { private set; get; }
    UIManager _ui;
    //AudioConroller
    public Inventory Inventory { get; private set; }
    public IsleManager IsleManager { get; private set; }
    public MainIsle MainIsle { get; private set; }

    [SerializeField] private Item _ropeItem;
    [SerializeField] private Item _uksusItem;
    [SerializeField] private Item _ore1;
    void Awake()
    {
        if(_instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);

        Player = TryToFindPlayer();
        IsleManager = TryToFindIsleManager();
        MainIsle = TryToFindMainIsle();
        Camera = TryToFindCameraManager();

        Inventory = new Inventory();
        Inventory.Add(_ropeItem, 4);
        Inventory.Add(_uksusItem, 1000);
        Inventory.Add(_ore1, 20);
    }

    private CameraManager TryToFindCameraManager()
    {
        try
        {
            return FindObjectOfType<CameraManager>();
        }
        catch
        {
            Debug.LogError("Camera Manager not found! Please, add Camera Manager to scene for correct gameplay!");
            return null;
        }
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

    private IsleManager TryToFindIsleManager()
    {
        try
        {
            return FindObjectOfType<IsleManager>();
        }
        catch
        {
            Debug.LogError("IsleManager not found! Please, add IsleManager to scene for correct gameplay!");
            return null;
        }
    }

    public MainIsle TryToFindMainIsle()
    {
        try
        {
            return FindObjectOfType<MainIsle>();
        }
        catch
        {
            Debug.LogError("MainIsle not found! Please, add MainIsle to scene for correct gameplay!");
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
