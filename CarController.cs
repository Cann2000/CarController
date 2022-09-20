using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float move;
    public float angle;


    [SerializeField] float MotorForce;
    [SerializeField] float BreakForce;
    [SerializeField] float SteerAngle;

    [SerializeField] Transform LeftFrontWhell;
    [SerializeField] Transform RightFronWhell;
    [SerializeField] Transform LeftRearWhell;
    [SerializeField] Transform RightRearWhell;

    [SerializeField] WheelCollider[] Wheels;


    private void Update()
    {
        CarController();
        UpdateWheels();

    }


    void CarController()
    {
        move = Input.GetAxis("Vertical");
        angle = Input.GetAxis("Horizontal");

        foreach (var wheel in Wheels)
        {
            if (move <= 0 || move >= 0)
            {
                wheel.brakeTorque = 0;
                wheel.motorTorque = move * MotorForce;
            }
            if (move == 0)
            {
                wheel.brakeTorque = BreakForce;
            }
        }
        for (int i = 0; i < Wheels.Length; i++)
        {
            if (i < 2)
            {
                Wheels[i].steerAngle = Input.GetAxis("Horizontal") * SteerAngle;
            }
        }
    }

    //void Breake()
    //{
    //    Wheels[0].brakeTorque = BreakForce;
    //    Wheels[1].brakeTorque = BreakForce;
    //    Wheels[2].brakeTorque = BreakForce;
    //    Wheels[3].brakeTorque = BreakForce;
    //}

    void UpdateWheels()
    {
        UpdateTurnWheels(Wheels[0], LeftFrontWhell);
        UpdateTurnWheels(Wheels[1], RightFronWhell);
        UpdateTurnWheels(Wheels[2], LeftRearWhell);
        UpdateTurnWheels(Wheels[3], RightRearWhell);
    }
    void UpdateTurnWheels(WheelCollider WheellCollider, Transform WhellTransform)
    {
        Vector3 pos;
        Quaternion rot;

        WheellCollider.GetWorldPose(out pos, out rot);

        WhellTransform.position = pos;
        WhellTransform.rotation = rot;
    }
}
