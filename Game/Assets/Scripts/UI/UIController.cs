using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIController : MonoBehaviour
{
    public UIType _uiType;

    abstract public void UpdateAll();
}
