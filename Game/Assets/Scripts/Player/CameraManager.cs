using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager _instance { private set; get; }
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private Camera _mainIsleCamera;

    private Camera _lastActiveCamera;

    public CameraType Type { private set; get; }
    public enum CameraType
    {
        Player,
        MainIsle
    }
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _mainIsleCamera.gameObject.SetActive(false);

        _lastActiveCamera = _playerCamera;
        Type = CameraType.Player;

        _mainIsleCamera.gameObject.SetActive(false);
    }

    public void ChangeCamera(CameraType type)
    {
        if (Type == type)
            return;

        Type = type;
        _lastActiveCamera.gameObject.SetActive(false);
        switch (type)
        {
            case CameraType.Player:
                _lastActiveCamera = _playerCamera;
                break;
            case CameraType.MainIsle:
                _lastActiveCamera = _mainIsleCamera;
                break;
        }
        _lastActiveCamera.gameObject.SetActive(true);
    }
}
