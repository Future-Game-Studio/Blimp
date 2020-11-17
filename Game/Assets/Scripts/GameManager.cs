using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Movement _player;
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
