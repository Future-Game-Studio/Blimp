using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : UIController
{
    [SerializeField] private TextMeshProUGUI _speed;
    [SerializeField] private TextMeshProUGUI _uksuses;
    private Inventory _inventory;

    private void OnEnable()
    {
        if(_inventory == null)
            _inventory = GameManager._instance.Inventory;

        GameManager._instance.Player.OnSpeedChanged += UpdateBlimpSpeed;

        UpdateUksuses();
    }

    public override void UpdateAll()
    {

    }

    private void UpdateBlimpSpeed(Movement.SpeedMode speedMode)
    {
        _speed.text = "Speed: " + speedMode.ToString();
    }

    public void UpdateUksuses()
    {
        _uksuses.text = "Uksuses: " + _inventory.GetUksusAmount();
    }

    private void OnDisable()
    {
        GameManager._instance.Player.OnSpeedChanged -= UpdateBlimpSpeed;
    }

    public void OpenMainMenu()
    {
        UIManager._instance.SwitchUI(UIType.MainMenu);
    }
}
