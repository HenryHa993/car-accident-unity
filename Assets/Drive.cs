using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drive : MonoBehaviour
{
    public WheelCollider WC;
    public float Torque = 200f;
    public float SteerAngle = 30f;
    public GameObject Wheel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WC = GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        UpdateMesh();
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        float acceleration = context.ReadValue<float>();
        Accelerate(acceleration);
    }

    void Accelerate(float acceleration)
    {
        // Accelerate wheel
        float thrustTorque = acceleration * Torque; // Should I process this here on OnAccelerate?
        WC.motorTorque = thrustTorque;
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        float steerAngle = context.ReadValue<float>() * SteerAngle;
        Steer(steerAngle);
    }

    private void Steer(float steerAngle)
    {
        WC.steerAngle = steerAngle;
    }

    private void UpdateMesh()
    {
        // Reposition game object to match motion
        Quaternion quat;
        Vector3 position;
        WC.GetWorldPose(out position, out quat);
        Wheel.transform.position = position;
        Wheel.transform.rotation = quat;
    }
}
