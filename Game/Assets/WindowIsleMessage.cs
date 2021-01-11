using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DefaultIsle))]
public class WindowIsleMessage : MonoBehaviour
{
    DefaultIsle _isle;
    [SerializeField] RectTransform _messagePrefab;
    [SerializeField] private UIType _uiType;

    private void Awake()
    {
        _isle = GetComponent<DefaultIsle>();
    }

    public GameObject InstantiateMessage()
    {
        GameObject messageObj = Instantiate(_messagePrefab, UIManager._instance.gameObject.transform as RectTransform).gameObject;

        var message = messageObj.GetComponent<UIIsleMessage>();

        message.SetSettings(_uiType, _isle);

        return messageObj;
    }
}
