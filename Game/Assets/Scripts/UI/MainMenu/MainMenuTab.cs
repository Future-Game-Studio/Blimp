using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainTabType
{
    Customization,
    Inventory,
    Map,
    Quests,
    Atlas,
    Settings
}

public abstract class MainMenuTab : MonoBehaviour
{
    [SerializeField] private MainTabType _tab;
    public MainTabType Tab { get => _tab; }
}
