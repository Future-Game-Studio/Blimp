using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class MainMenu : UIController
{
    private List<MainMenuTab> _tabs;

    private MainMenuTab _lastActiveTab;

    private void Start()
    {
        _tabs = GetComponentsInChildren<MainMenuTab>().ToList();

        _tabs.ForEach(t => t.gameObject.SetActive(false));

        SwitchTab(MainTabType.Customization);
    }
    public override void UpdateAll()
    {
        throw new System.NotImplementedException();
    }

    public MainMenuTab SwitchTab(MainTabType type)
    {
        if (_lastActiveTab != null)
        {
            _lastActiveTab.gameObject.SetActive(false);
        }

        MainMenuTab desiredUI = _tabs.Find(t => t.Tab == type);
        if (desiredUI != null)
        {
            desiredUI.gameObject.SetActive(true);
            _lastActiveTab = desiredUI;
        }
        else Debug.LogError("Can't find the ui object!");

        return desiredUI;
    }

    public void SwitchTab(int i)
    {
        if (_lastActiveTab != null)
        {
            _lastActiveTab.gameObject.SetActive(false);
        }

        MainMenuTab desiredUI = _tabs[i];
        if (desiredUI != null)
        {
            desiredUI.gameObject.SetActive(true);
            _lastActiveTab = desiredUI;
        }
        else Debug.LogError("Can't find the ui object!");

    }

}
