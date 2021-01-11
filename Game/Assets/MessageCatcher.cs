using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCatcher : MonoBehaviour
{
    private GameObject _message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WindowIsleMessage>() != null)
            if (UIManager._instance.IsHUD)
            {
                var thrower = other.gameObject.GetComponent<WindowIsleMessage>();
                _message = thrower.InstantiateMessage();
                UIManager._instance.OnUIChanged += TryToCloseMessage;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        CloseMessage();
    }

    private void DisableMessage()
    {
        if (_message != null)
            _message.SetActive(false);
    }

    private void EnableMessage()
    {
        if (_message != null)
            _message.SetActive(true);
    }

    private void CloseMessage()
    {
        if (_message != null)
        {
            Destroy(_message);
            UIManager._instance.OnUIChanged -= TryToCloseMessage;
        }
    }

    private void TryToCloseMessage(UIType newUI)
    {
        if (newUI != UIType.HUD)
            DisableMessage();
        else
            EnableMessage();
    }
}