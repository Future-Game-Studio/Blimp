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
    public Inventory Inventory { get; private set; }
    public IsleManager IsleManager { get; private set; }
    public MainIsle MainIsle { get; private set; }

    [SerializeField] private Item _ropeItem;
    void Awake()
    {
        if(_instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);

        Player = TryToFindPlayer();
        IsleManager = TryToFindIsleManager();
        MainIsle = TryToFindMainIsle();

        Inventory = new Inventory();
        Inventory.Add(_ropeItem, 1);
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
