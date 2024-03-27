using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{
    private Rigidbody playerRB;
    public WheelColliders colliders;
    public WheelMesh meshes;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;
    public WheelParticles wheelParticles;
    public GameObject smokePrefab;

    [SerializeField] private float motorPower;
    [SerializeField] private float brakePower;
    private float slipAngle;
    private float speed;
    public AnimationCurve steeringCurve;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        InstantiateSmoke();
    }
    private void FixedUpdate()
    {
        speed = playerRB.velocity.magnitude;
        ApplyMotor();
        CheckInput();
        ApplyWheelPositions();
        ApplySteering();
        ApplyBrake();
        CheckParticles();
    }

    private void ApplyMotor()
    {
        colliders.rearRightCollider.motorTorque = -motorPower * gasInput;
        colliders.rearLeftCollider.motorTorque = -motorPower * gasInput;
    }
    private void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        if (slipAngle < 60f)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, playerRB.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.frontRightCollider.steerAngle = steeringAngle;
        colliders.frontLeftCollider.steerAngle = steeringAngle;

    }
   private void InstantiateSmoke()
    {
        wheelParticles.frontRightCollider = Instantiate(smokePrefab, colliders.frontRightCollider.transform.position - Vector3.up * colliders.frontRightCollider.radius, Quaternion.identity, colliders.frontRightCollider.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.frontLeftCollider = Instantiate(smokePrefab, colliders.frontLeftCollider.transform.position - Vector3.up * colliders.frontRightCollider.radius, Quaternion.identity, colliders.frontLeftCollider.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.rearRightCollider = Instantiate(smokePrefab, colliders.rearRightCollider.transform.position - Vector3.up * colliders.frontRightCollider.radius, Quaternion.identity, colliders.rearRightCollider.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.rearLeftCollider = Instantiate(smokePrefab, colliders.rearLeftCollider.transform.position - Vector3.up * colliders.frontRightCollider.radius, Quaternion.identity, colliders.rearLeftCollider.transform)
            .GetComponent<ParticleSystem>();
    }

    private void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
        slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);
        if (slipAngle < 120f)
        {
            if (gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
                gasInput = 0;
            }
            else
            {
                brakeInput = 0;
            }
        }
        else
        {
            brakeInput = 0;
        }
    }
    void CheckParticles()
    {
        WheelHit[] wheelHits = new WheelHit[4];
        colliders.frontRightCollider.GetGroundHit(out wheelHits[0]);
        colliders.frontLeftCollider.GetGroundHit(out wheelHits[1]);

        colliders.rearRightCollider.GetGroundHit(out wheelHits[2]);
        colliders.rearLeftCollider.GetGroundHit(out wheelHits[3]);

        float slipAllowance = 0.5f;
        if ((Mathf.Abs(wheelHits[0].sidewaysSlip) + Mathf.Abs(wheelHits[0].forwardSlip) > slipAllowance))
        {
            wheelParticles.frontRightCollider.Play();
        }
        else
        {
            wheelParticles.frontRightCollider.Stop();
        }
        if ((Mathf.Abs(wheelHits[1].sidewaysSlip) + Mathf.Abs(wheelHits[1].forwardSlip) > slipAllowance))
        {
            wheelParticles.frontLeftCollider.Play();
        }
        else
        {
            wheelParticles.frontLeftCollider.Stop();
        }
        if ((Mathf.Abs(wheelHits[2].sidewaysSlip) + Mathf.Abs(wheelHits[2].forwardSlip) > slipAllowance))
        {
            wheelParticles.rearRightCollider.Play();
        }
        else
        {
            wheelParticles.rearRightCollider.Stop();
        }
        if ((Mathf.Abs(wheelHits[3].sidewaysSlip) + Mathf.Abs(wheelHits[3].forwardSlip) > slipAllowance))
        {
            wheelParticles.rearLeftCollider.Play();
        }
        else
        {
            wheelParticles.rearLeftCollider.Stop();
        }


    }
    private void ApplyBrake()
    {
        colliders.frontRightCollider.brakeTorque = brakeInput * -brakePower * 0.7f;
        colliders.frontLeftCollider.brakeTorque = brakeInput * -brakePower * 0.7f;

        colliders.rearRightCollider.brakeTorque = brakeInput * -brakePower * 0.3f;
        colliders.rearLeftCollider.brakeTorque = brakeInput * -brakePower * 0.3f;

    }
    private void ApplyWheelPositions()
    {
        UpdateWheel(colliders.frontLeftCollider, meshes.frontLeftCollider);
        UpdateWheel(colliders.frontRightCollider, meshes.frontRightCollider);
        UpdateWheel(colliders.rearLeftCollider, meshes.rearLeftCollider);
        UpdateWheel(colliders.rearRightCollider, meshes.rearRightCollider);
    }
    private void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position; 
        wheelMesh.transform.rotation = quat;
    }
}

[Serializable]
public class WheelColliders 
{
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;
}
[Serializable]
public class WheelMesh
{
    public MeshRenderer frontLeftCollider;
    public MeshRenderer frontRightCollider;
    public MeshRenderer rearLeftCollider;
    public MeshRenderer rearRightCollider;
}

[Serializable]
public class WheelParticles
{
    public ParticleSystem frontRightCollider;
    public ParticleSystem frontLeftCollider;
    public ParticleSystem rearRightCollider;
    public ParticleSystem rearLeftCollider;

}
