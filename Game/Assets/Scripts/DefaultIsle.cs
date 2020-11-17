using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultIsle : MonoBehaviour
{
    [SerializeField] private uint _id;
    public Vector3 _position { get; private set; }


    private void Start()
    {
        if (_position == null)
            _position = transform.position;

        Debug.Log("Isle: " + _id + " - " + transform.name + " - " + transform.position);
    }
}
