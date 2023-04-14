using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class CashRegister : MonoBehaviour
{
    public float speed;
    public Vector3 move;
    public Rigidbody rb;
    private bool isPressed = false;

    [SerializeField]
    private bool left;
    [SerializeField]
    private bool right;
    [SerializeField]
    private bool up;
    [SerializeField]
    private bool down;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10f;
        move = new();
        if (left)
        {
            move.x = -1;
        }
        if (right)
        {
            move.x = 1;
        }
        if (down)
        {
            move.z = -1;
        }
        if (up)
        {
            move.z = 1;
        }
        move *= speed;
    }

    public void PushOut()
    {
        if (isPressed == false)
        {
            rb.velocity = move;
        }

        else if (isPressed == true)
        {
            rb.velocity = move * -1;
        }

        isPressed = !isPressed;
    }
}