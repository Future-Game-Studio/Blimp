using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SelectableButton : MonoBehaviour
{
    [SerializeField] protected Button _button;
    public Button Button { get => _button; }
    public bool IsActive { get; private set; }

    public abstract void Activate();
    public abstract void Deactivate();

}
