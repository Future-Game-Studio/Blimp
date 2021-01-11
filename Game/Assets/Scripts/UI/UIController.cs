using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIController : MonoBehaviour
{
    [SerializeField] private UIType _uiType;
    public UIType UIType { get => _uiType; }

    abstract public void UpdateAll();
}
