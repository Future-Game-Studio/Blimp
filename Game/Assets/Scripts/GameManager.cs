using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    movement _player;
    CameraManager _camera;
    UIController _ui;
    //AudioConroller
    OwnedItems ownedItems;
    IslesManager _islesManager;
    BlimpProperties _blimpProps;

    void Start()
    {
        _player = TryToFindPlayer();
    }



    void Update()
    {

    }


    private movement TryToFindPlayer()
    {
        try
        {
            return GameObject.FindObjectOfType<movement>();
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
