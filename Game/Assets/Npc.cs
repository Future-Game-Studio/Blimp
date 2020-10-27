using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Npc : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogue;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");
            dialogue.SetActive(true);
        }
    }
}
