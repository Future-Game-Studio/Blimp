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

    [SerializeField] Item item1;
    [SerializeField] Item item2;
    [SerializeField] Item item3;

    void Awake()
    {
        if(_instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log(_instance);

        Player = TryToFindPlayer();
        IsleManager = TryToFindIsleManager();

        Inventory = new Inventory();

        Inventory.Items.AddItem(item3, 3);
        Inventory.Items.AddItem(item1, 5);
        Inventory.Items.AddItem(item1, 5);
        Inventory.Items.AddItem(item2, 4);
        //Inventory.Items.DebugItems();
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
