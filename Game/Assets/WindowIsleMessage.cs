using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DefaultIsle))]
public class WindowIsleMessage : MonoBehaviour
{
    DynamicIsle _isle;
    [SerializeField] RectTransform _messagePrefab;
    [SerializeField] private UIType _uiType;

    private void Awake()
    {
        _isle = GetComponent<DynamicIsle>();
    }

    public GameObject InstantiateMessage()
    {
        GameObject messageObj = Instantiate(_messagePrefab, UIManager._instance.gameObject.transform as RectTransform).gameObject;

        var message = messageObj.GetComponent<UIIsleMessage>();

        string name = _isle.Info?.name ?? "No name isle";
        string type = _isle.Info?._type.ToString() ?? "Empty";
        string description = _isle.Info?._description ?? "Dock this isle to use it!";

        message.SetSettings(_uiType, _isle, name, description, type);

        return messageObj;
    }
}
