using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float move;
    public float angle;

    public GameObject Karakter;

    public bool ArabayaBinildi;

    [SerializeField] OyunAyarları oyunayarları;

    [SerializeField] float MotorForce;
    [SerializeField] float BreakForce;
    [SerializeField] float SteerAngle;

    [SerializeField] Transform SolOnWhell;
    [SerializeField] Transform SagOnWhell;
    [SerializeField] Transform SolArkaWhell;
    [SerializeField] Transform SagArkaWhell;

    [SerializeField] WheelCollider[] Wheels;

    public GameObject centerofmass;
    public Rigidbody rb;
    void Start()
    {
        Karakter = GameObject.FindGameObjectWithTag("Player");
        oyunayarları = GameObject.FindGameObjectWithTag("Player").GetComponent<OyunAyarları>();
        rb.centerOfMass = centerofmass.transform.localPosition;
    }

    private void Update()
    {
        Arabadanin();
    }

    private void FixedUpdate()
    {
        if (ArabayaBinildi)
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

        if (!ArabayaBinildi)
        {
            foreach (var wheel in Wheels)
            {
                wheel.brakeTorque = BreakForce;
            }
        }

        UpdateWheels();
    }

    void Arabadanin()
    {
        if (ArabayaBinildi)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.transform.GetChild(0).GetComponent<Camera>().enabled = false;
                Karakter.SetActive(true);
                gameObject.GetComponent<Rigidbody>().mass = 100;
                ArabayaBinildi = !ArabayaBinildi;
                oyunayarları.arabaybin = !oyunayarları.arabaybin;
                for (int i = 0; i < oyunayarları.ArabadaKapanıcakPaneller.Length; i++)
                {
                    oyunayarları.ArabadaKapanıcakPaneller[i].SetActive(true);
                }
            }
        }
    }

    //void fren()
    //{
    //    Wheels[0].brakeTorque = BreakForce;
    //    Wheels[1].brakeTorque = BreakForce;
    //    Wheels[2].brakeTorque = BreakForce;
    //    Wheels[3].brakeTorque = BreakForce;
    //}

    void UpdateWheels()
    {
        UpdateTurnWheels(Wheels[0], SolOnWhell);
        UpdateTurnWheels(Wheels[1], SagOnWhell);
        UpdateTurnWheels(Wheels[2], SolArkaWhell);
        UpdateTurnWheels(Wheels[3], SagArkaWhell);
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
