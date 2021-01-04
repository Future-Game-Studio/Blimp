using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public enum SpeedMode { Backwards = -3, Idle = 0, Low = 3, Medium = 5, High = 8 };

    public Text GearText;

    public float shipRotateSpeed;
    public SpeedMode speedMode;
    public float lastSpeed;
    public Rigidbody rb;
    Vector3 m_EulerAngleVelocity;
    [SerializeField] private Transform _connectionPoint;
    public Transform ConnectionPoint { get => _connectionPoint; }

    void Start()
    {
        m_EulerAngleVelocity = new Vector3(0, shipRotateSpeed, 0);
        rb = GetComponent<Rigidbody>();
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

        lastSpeed += lastSpeed == (int)speedMode ? 0 : lastSpeed < (int)speedMode ? 0.1f : -0.1f;
        lastSpeed = (speedMode == SpeedMode.Idle && Mathf.Abs(lastSpeed) < 0.25f) ? 0 : lastSpeed;

        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * horizontal);
        rb.MoveRotation(rb.rotation * deltaRotation);

        rb.velocity = transform.forward * lastSpeed;
        
    }

    void ChangeSpeedUp()
    {
        switch (speedMode)
        {
            case SpeedMode.Backwards:
                speedMode = SpeedMode.Idle;
                //GearText.text = "Idle";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Idle:
                speedMode = SpeedMode.Low;
                //GearText.text = "Low";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Low:
                speedMode = SpeedMode.Medium;
                //GearText.text = "Medium";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Medium:
                speedMode = SpeedMode.High;
                //GearText.text = "High";
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
                //GearText.text = "Medium";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Medium:
                speedMode = SpeedMode.Low;
                //GearText.text = "Low";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Low:
                speedMode = SpeedMode.Idle;
                //GearText.text = "Idle";
                Debug.Log("Speed mode " + speedMode);
                return;
            case SpeedMode.Idle:
                speedMode = SpeedMode.Backwards;
                //GearText.text = "Backwards";
                Debug.Log("Speed mode " + speedMode);
                return;
            default:
                return;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        //GearText.text = "Idle";
        speedMode = SpeedMode.Idle;
    }
}
