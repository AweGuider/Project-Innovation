using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviourPunCallbacks
{
    private Vector3 oldAcceleration;
    private Vector3 oldGyroscope;

    private CharacterController cc;
    private Vector3 move;
    private float x;
    private float z;

    PhotonView view;

    [SerializeField] private float speed;
    [SerializeField] private float maxVelocityChange;
    private Rigidbody rb;
    private Vector2 input;

    private void Start()
    {
        speed = 4f;
        maxVelocityChange = 10f;

        oldAcceleration = Input.acceleration;
        oldGyroscope = Input.gyro.rotationRate;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        cc = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();

        rb = GetComponent<Rigidbody>();

#if PC
        //Nothing yet
#elif PHONE
        Input.gyro.enabled = true;
#endif
//#if PHONE
//        gameObject.AddComponent<Chara>
//        cc = Add
    }

    private void Update()
    {
        if (view.IsMine)
        {
#if PC
            UpdateKeyboard();
#elif PHONE
            UpdateSensors();
#endif
        }
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
#if PC
            rb.AddForce(CalculateMovement(speed), ForceMode.VelocityChange);
#endif

#if PHONE
            UpdateSensors();
#endif
        }
    }


    private void UpdateKeyboard()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();
        //move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //cc.Move(5 * Time.deltaTime * move);
    }

    private void UpdateSensors()
    {
        Vector3 acceleration = Input.acceleration;
        Vector3 gyroscope = Input.gyro.rotationRate;
        if (acceleration != oldAcceleration || gyroscope != oldGyroscope)
        {
            oldAcceleration = acceleration;
            oldGyroscope = gyroscope;
            cc.Move(5 * Time.deltaTime * acceleration);
            //SendInput(acceleration, gyroscope);
        }
    }
    private Vector3 CalculateMovement(float speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= speed;

        Vector3 velocity = rb.velocity;

        if (input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            velocityChange.y = 0;

            return velocityChange;
        }
        else
        {
            return new Vector3();
        }
    }

    public void SendInput(Vector3 acceleration, Vector3 gyroscope)
    {
        photonView.RPC("ReceiveInput", RpcTarget.MasterClient, acceleration, gyroscope);
    }
}
