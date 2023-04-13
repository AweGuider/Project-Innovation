using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviourPunCallbacks
{
    [Header("Player Related")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rb;

    [Header("Sensors related")]
    private Vector3 oldAcceleration;
    private Vector3 oldGyroscope;
    private Vector3 initialOffset;
    [SerializeField] private float minPosAngle;
    [SerializeField] private float maxPosAngle;
    [SerializeField] private float minNegAngle;
    [SerializeField] private float maxNegAngle;

    [Header("Boost Related")]
    [SerializeField] private float speed;
    [SerializeField] private float speedBoost;
    [SerializeField] private float speedBoostMax;
    [SerializeField] private float boostDuration;
    [SerializeField] private float boostCooldown;
    [SerializeField] private bool isBoosting;
    [SerializeField] private Button boostButton;


    private void Start()
    {
        // General
        speed = 5f;
        speedBoost = 1f;
        speedBoostMax = 2f;
        boostDuration = 5f;
        boostCooldown = 10f;

        // Phone
        if (boostButton != null) boostButton.onClick.AddListener(Boost);

        oldAcceleration = Input.acceleration;
        oldGyroscope = Input.gyro.rotationRate;
        initialOffset = new(0, Mathf.Sin(-60f * Mathf.Deg2Rad), 0);
        //initialOffset = initialOffset.normalized;
        minPosAngle = 20f;
        maxPosAngle = 90f;
        minNegAngle = -20f;
        maxNegAngle = -90f;

#if PC
        //Nothing yet
#elif PHONE
        Input.gyro.enabled = true;
#endif
    }



    private void FixedUpdate()
    {
        try
        {
            if (player == null) player = GameObject.FindGameObjectWithTag("Player");
            if (rb == null) rb = player.GetComponent<Rigidbody>();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
#if PC
        if (Input.GetKeyDown(KeyCode.Space) && !isBoosting)
        {
            Boost();
        }
#endif
        //if (photonView.IsMine)
        //{
        Debug.Log($"VIEW IS MINE!");
            Vector3 move;
            //move = UpdateKeyboard();

#if PC
            move = UpdateKeyboard();
#elif PHONE
            move = UpdateSensors();
#endif
            Vector3 moveDirection = move.normalized;
            Quaternion rotationChange;
            if (moveDirection.magnitude > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                rotationChange = Quaternion.Slerp(player.transform.rotation, targetRotation, 5f * Time.deltaTime);
                player.transform.rotation = rotationChange;
            }
            else
            {
                rotationChange = player.transform.rotation;
            }
            moveDirection *= speed * speedBoost * Time.deltaTime;

            photonView.RPC("UpdatePosition", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, moveDirection, rotationChange);

        //}
    }

    private Vector3 UpdateKeyboard()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return new(x, 0, z);
    }

    private Vector3 UpdateSensors()
    {
        // Receive input
        Vector3 acceleration = Input.acceleration;
        Vector3 gyroscope = Input.gyro.rotationRate;


        float xDeg = Mathf.Asin(acceleration.x) * Mathf.Rad2Deg;
        float yDeg = Mathf.Asin(acceleration.y) * Mathf.Rad2Deg;

        float xMag = CalculateMagnitude(xDeg);
        float zMag = CalculateMagnitude(yDeg);

        //Vector3 move = new(xMag, 0, zMag);
        Vector3 move = new(acceleration.x, 0, acceleration.y);

        return move;
    }

    private float CalculateMagnitude(float angle)
    {
        float magnitude = 0;
        if (angle < minNegAngle)
        {
            magnitude = Mathf.Clamp(angle, maxNegAngle, minNegAngle) / maxNegAngle;
        }
        else if (angle > minPosAngle)
        {
            magnitude = Mathf.Clamp(angle, minPosAngle, maxPosAngle) / maxPosAngle;
        }
        return magnitude;
    }
    private void Boost()
    {
        StartCoroutine(BoostCoroutine());
    }
    IEnumerator BoostCoroutine()
    {
        isBoosting = true;
        speedBoost = speedBoostMax;
        yield return new WaitForSeconds(boostDuration);
        speedBoost = 1f;
        isBoosting = false;
        yield return new WaitForSeconds(boostCooldown);
    }

    [PunRPC]
    public void UpdatePosition(Vector3 pos)
    {
        //rb.AddForce(pos, ForceMode.Impulse);

        player.transform.position = pos;
    }
}
