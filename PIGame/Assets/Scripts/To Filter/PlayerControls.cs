using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class PlayerControls : MonoBehaviour
//{
//    private Vector3 move;

//    //Movement control
//    private Rigidbody rb;
//    private CharacterController cc;
//    [SerializeField]
//    private float movementForce = 1f;
//    [SerializeField]
//    private float jumpForce = 5f;
//    [SerializeField]
//    private float maxSpeed = 5f;
//    private Vector3 forceDirection = Vector3.zero;

//    [SerializeField]
//    private Camera playerCamera;

//    [SerializeField]
//    private int jumpNr = 2;

//    //private void Awake()
//    //{
//    //    rb = GetComponent<Rigidbody>();
//    //    cc = GetComponent<CharacterController>();
//    //}

//    //private void FixedUpdate()
//    //{
//    //    //move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//    //    //cc.Move(move * Time.deltaTime * maxSpeed);

//    //    //LookAt();
//    //}

//    //private void LookAt()
//    //{
//    //    Vector3 direction = rb.velocity;
//    //    direction.y = 0;

//    //    if (move.sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
//    //    {
//    //        this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
//    //    }
//    //    else
//    //    {
//    //        rb.angularVelocity = Vector3.zero;
//    //    }
//    //}
//}
