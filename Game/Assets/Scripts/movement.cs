using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public enum SpeedMode { Backwards = -1, Idle = 0, Low = 1, Medium = 2, High = 3 };


    public Transform cam;
    public float shipRotateSpeed;
    public float shipSpeed = 6f;
    public SpeedMode speedMode;
    public float lastSpeed;

    public float turnSmoothTime = 0.1f;

    void Start()
    {
        speedMode = SpeedMode.Idle;
        lastSpeed = (int)speedMode;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeSpeedUp();
        }
        else if (Input.GetKeyDown(KeyCode.S))
            ChangeSpeedDown();
    }


    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Rotate(horizontal);

        lastSpeed += lastSpeed == (int)speedMode ? 0 : lastSpeed < (int)speedMode ? 0.1f : -0.1f;
        lastSpeed = (speedMode == SpeedMode.Idle && Mathf.Abs(lastSpeed) < 0.25f) ? 0 : lastSpeed;
        transform.Translate(Vector3.forward * lastSpeed * 0.1f, Space.Self);

    }



    void ChangeSpeedUp()
    {
        switch (speedMode)
        {
            case SpeedMode.Backwards:
                speedMode = SpeedMode.Idle;
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Idle:
                speedMode = SpeedMode.Low;
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Low:
                speedMode = SpeedMode.Medium;
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Medium:
                speedMode = SpeedMode.High;
                Debug.Log("Speed mode " + speedMode);
                return;
            default:
                return;
        }
    }

    void ChangeSpeedDown()
    {
        switch (speedMode)
        {
            case SpeedMode.High:
                speedMode = SpeedMode.Medium;
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Medium:
                speedMode = SpeedMode.Low;
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Low:
                speedMode = SpeedMode.Idle;
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Idle:
                speedMode = SpeedMode.Backwards;
                Debug.Log("Speed mode " + speedMode);
                return;
            default:
                return;
        }
    }

    void Rotate(float horizontal)
    {
        transform.Rotate(Vector3.up * shipRotateSpeed * horizontal);
    }
}
