using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum UIType
{
    StartMenu,
    MainMenu,
    HUD,
    ResourceIsle,
    QuestIsle,
    MainIsle
}

public class UIManager : MonoBehaviour
{
    public static UIManager _instance { private set; get; }

    List<UIController> _uiControllers;
    UIController _lastActiveUI;
    public DefaultIsle LastActiveIsle { get; private set; }
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(gameObject);

        _uiControllers = GetComponentsInChildren<UIController>().ToList();

        _uiControllers.ForEach(c => c.gameObject.SetActive(false));

        SwitchUI(UIType.HUD);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchUI(UIType.HUD);
    }

    public UIController SwitchUI(UIType type)
    {
        if(_lastActiveUI != null)
        {
            _lastActiveUI.gameObject.SetActive(false);
        }

        UIController desiredUI = _uiControllers.Find(c => c._uiType == type);
        if (desiredUI != null)
        {
            desiredUI.gameObject.SetActive(true);
            _lastActiveUI = desiredUI;
        }
        else Debug.LogError("Can't find the ui object!");

        return desiredUI;
    }

    public void SwitchIsleUI(UIType type, DefaultIsle isle)
    {
        UIController desiredUI = SwitchUI(type);
        LastActiveIsle = isle;
        desiredUI.UpdateAll();
    }

}
