using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float move;
    public float angle;

    public GameObject Karakter;

    public bool ArabayaBinildi;

    [SerializeField] OyunAyarlarý oyunayarlarý;

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
        oyunayarlarý = GameObject.FindGameObjectWithTag("Player").GetComponent<OyunAyarlarý>();
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
                    hit.transform.parent.GetChild(0).GetComponent<Camera>().enabled = true;
                    //hit.transform.GetChild(0).GetComponent<Camera>().enabled = true;
                    gameObject.transform.SetParent(hit.transform.parent);
                    hit.transform.parent.GetComponent<CarController>().ArabayaBinildi = true;
                    //hit.transform.GetComponent<CarController>().ArabayaBinildi = true;
                    //hit.transform.GetComponent<Rigidbody>().mass = 1000;
                    hit.transform.parent.GetComponent<Rigidbody>().mass = 1000;
                    gameObject.SetActive(false);
                for (int i = 0; i < oyunayarlarý.ArabadaKapanýcakPaneller.Length; i++)
                {
                    oyunayarlarý.ArabadaKapanýcakPaneller[i].SetActive(true);
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
