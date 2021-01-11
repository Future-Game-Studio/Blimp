using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIsleCamera : MonoBehaviour
{
    [SerializeField] private Transform _minY;
    [SerializeField] private Transform _maxY;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _panLimit;

    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _panSpeed;
    [SerializeField] private float _panBorderThickness;

    private void OnGUI()
    {
        Vector3 targetPos = _target.localPosition;

        targetPos.y += -Input.mouseScrollDelta.y * _scrollSpeed * Time.deltaTime;

        targetPos.y = Mathf.Clamp(targetPos.y, _minY.position.y, _maxY.position.y);
        

        if (Input.GetKey("w") || Input.mousePosition.y > Screen.height - _panBorderThickness)
        {
            targetPos.z += _panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y < _panBorderThickness)
        {
            targetPos.z -= _panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x < _panBorderThickness)
        {
            targetPos.x -= _panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x > Screen.width - _panBorderThickness)
        {
            targetPos.x += _panSpeed * Time.deltaTime;
        }

        targetPos.x = Mathf.Clamp(targetPos.x, -_panLimit.x, _panLimit.x);
        targetPos.z = Mathf.Clamp(targetPos.z, -_panLimit.y, _panLimit.y);

        _target.transform.localPosition = targetPos;




    }

}
