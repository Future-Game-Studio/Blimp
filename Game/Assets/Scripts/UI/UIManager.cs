using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DapperDino.TooltipUI;

public enum UIType
{
    StartMenu,
    MainMenu,
    HUD,
    ResourceIsle,
    CraftIsle,
    QuestIsle,
    MainIsle
}

public class UIManager : MonoBehaviour
{
    public static UIManager _instance { private set; get; }
    public TooltipPopup TIP { private set; get; }
    private List<UIController> _uiControllers;
    private UIController _lastActiveUI;

    public DefaultIsle LastActiveIsle { get; private set; }

    public delegate void EscapeDelegate();
    public EscapeDelegate OnEscape;

    public delegate void UITypeDelegate(UIType newType);
    public UITypeDelegate OnUIChanged;

    public bool IsHUD { get => _lastActiveUI.UIType == UIType.HUD; }
    public bool IsMainIsle { get => _lastActiveUI.UIType == UIType.MainIsle; }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(gameObject);

        _uiControllers = GetComponentsInChildren<UIController>().ToList();

        _uiControllers.ForEach(c => c.gameObject.SetActive(false));

        SwitchUI(UIType.HUD);

        TIP = FindObjectOfType<TooltipPopup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchUI(UIType.MainMenu);
        }


    }

    public UIController SwitchUI(UIType type)
    {
        if(_lastActiveUI != null)
        {
            _lastActiveUI.gameObject.SetActive(false);
        }

        UIController desiredUI = _uiControllers.Find(c => c.UIType == type);
        if (desiredUI != null)
        {
            desiredUI.gameObject.SetActive(true);
            _lastActiveUI = desiredUI;
        }
        else Debug.LogError("Can't find the ui object!");

        OnUIChanged?.Invoke(_lastActiveUI.UIType);

        return desiredUI;
    }

    public void SwitchIsleUI(UIType type, DefaultIsle isle)
    {
        UIController desiredUI = SwitchUI(type);
        LastActiveIsle = isle;
        desiredUI.UpdateAll();
    }

}
