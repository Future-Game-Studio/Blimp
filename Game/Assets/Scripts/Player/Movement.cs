using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public enum SpeedMode { Backwards = -1, Idle = 0, Low = 1, Medium = 2, High = 3 };

    public Text GearText;

    public float shipRotateSpeed;
    public float shipSpeed = 6f;
    public SpeedMode speedMode;
    public float lastSpeed;


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
                GearText.text = "Idle";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Idle:
                speedMode = SpeedMode.Low;
                GearText.text = "Low";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Low:
                speedMode = SpeedMode.Medium;
                GearText.text = "Medium";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Medium:
                speedMode = SpeedMode.High;
                GearText.text = "High";
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
                GearText.text = "Medium";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Medium:
                speedMode = SpeedMode.Low;
                GearText.text = "Low";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Low:
                speedMode = SpeedMode.Idle;
                GearText.text = "Idle";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Idle:
                speedMode = SpeedMode.Backwards;
                GearText.text = "Backwards";
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

    void OnCollisionEnter(Collision collision)
    {

        GearText.text = "Idle";
        speedMode = SpeedMode.Idle;
    }
}
