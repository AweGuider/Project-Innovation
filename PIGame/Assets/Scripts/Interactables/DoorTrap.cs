using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrap : Trap
{
    //private bool open = false;
    [SerializeField]
    private Animator anim;

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            Open_Door();
            Debug.Log("F");
        }*/
    }

    void Open_Door()
    {
   
    }

    public override void ActivateTrap()
    {
        if (anim.GetBool("Open") == false)
        {
            anim.SetBool("Open", true);
        }
        else
        {
            anim.SetBool("Open", false);
        }
    }
}
