using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputController : MonoBehaviourPunCallbacks
{
    private Vector3 oldAcceleration;
    private Vector3 oldGyroscope;
    private Vector3 innitialOffset;
    public float maxTiltAngle;

    private CharacterController cc;
    private Vector3 move;
    private float x;
    private float z;

    PhotonView view;

    [SerializeField] private float speed;
    [SerializeField] private float maxVelocityChange;
    private Rigidbody rb;
    private Vector2 input;

    [SerializeField]
    private TextMeshProUGUI accText;
    [SerializeField]
    private TextMeshProUGUI angleText;

    private void Start()
    {
        // General
        speed = 10f;

        // Phone
        oldAcceleration = Input.acceleration;
        oldGyroscope = Input.gyro.rotationRate;
        innitialOffset = new(0, 1, 0);
        maxTiltAngle = 75f;

        // PC
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        // Old
        maxVelocityChange = 10f;
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

    private void FixedUpdate()
    {
//        Debug.Log($"UPDATE!");
//#if PC
//        UpdateKeyboard();
//#elif PHONE
//        UpdateSensors();
//#endif

        if (view.IsMine)
        {
            Debug.Log($"VIEW IS MINE!");

#if PC
            UpdateKeyboard();
#elif PHONE
            UpdateSensors();
#endif
        }
    }


    private void UpdateKeyboard()
    {
        float x = Input.GetAxis("Horizontal") * 10f * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * 10f * Time.deltaTime;
        transform.Translate(x, 0, z);
        //input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //input.Normalize();
        //move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //cc.Move(5 * Time.deltaTime * move);
    }

    private void UpdateSensors()
    {
        // Receive input
        Vector3 acceleration = Input.acceleration + innitialOffset;
        Vector3 gyroscope = Input.gyro.rotationRate;

        // Calculate the tilt angle of the phone.
        float tiltAngle = Vector3.Angle(Vector3.up, acceleration);
        angleText.text = $"{tiltAngle}";

        // Map the tilt angle to a range of acceleration values.
        float accelerationMagnitude = Mathf.Clamp(tiltAngle - 15f, 0f, maxTiltAngle - 15f) / (maxTiltAngle - 15f);

        // Multiply by speed, magnitude and time
        acceleration *= speed * accelerationMagnitude * Time.deltaTime;
        accText.text = $"({acceleration.x}, 0, {acceleration.y})";

        transform.Translate(new Vector3(acceleration.x, 0, acceleration.y));
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

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0;

        if (move.sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void SendInput(Vector3 acceleration, Vector3 gyroscope)
    {
        photonView.RPC("ReceiveInput", RpcTarget.MasterClient, acceleration, gyroscope);
    }

    // Filter later

    //[PunRPC]
    //private void UpdatePosition(Vector3 pos)
    //{
    //    position = pos;
    //}

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(position);
    //    }
    //    else if (stream.IsReading)
    //    {
    //        position = (Vector3)stream.ReceiveNext();
    //    }
    //}

    //[PunRPC]
    //public void ReceiveInput(Vector3 acceleration, Vector3 gyroscope)
    //{
    //    // Do something with the input data, e.g. update the player's movement
    //}
}
