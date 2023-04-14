using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class CashRegister2 : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody rb;
    private bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PushOut()
    {
        if (isPressed == false)
        {
            rb.velocity = new Vector3(0, 0, 10);
            isPressed = true;
        }

        else if (isPressed == true)
        {
            rb.velocity = new Vector3(0, 0, -10);
            isPressed = false;
        }
    }
}