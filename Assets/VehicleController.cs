using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VehicleController : MonoBehaviour
{
    // The functions to update wheel colliders could be encapsulated in a class which manages both?
    public WheelCollider[] WheelColliders;
    public WheelCollider[] SteeringWheelColliders;
    public GameObject[] WheelMeshes;

    public float Torque = 200f;
    public float SteerAngleDeg = 30f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        UpdateMeshes();
    }

    private void UpdateMeshes()
    {
        for (int i = 0; i < WheelColliders.Length; i++)
        {
            Quaternion quat;
            Vector3 pos;
            WheelColliders[i].GetWorldPose(out pos, out quat);
            WheelMeshes[i].transform.position = pos;
            WheelMeshes[i].transform.rotation = quat;
        }
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        float acceleration = context.ReadValue<float>() * Torque;
        Accelerate(acceleration);
    }

    private void Accelerate(float acceleration)
    {
        foreach (WheelCollider wheelCollider in WheelColliders)
        {
            wheelCollider.motorTorque = acceleration;
        }
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        float steerAngle = context.ReadValue<float>() * SteerAngleDeg;
        Steer(steerAngle);
    }

    private void Steer(float steerAngle)
    {
        foreach (WheelCollider wheelCollider in SteeringWheelColliders)
        {
            wheelCollider.steerAngle = steerAngle;
        }
    }
}
