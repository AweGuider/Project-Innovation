using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviourPunCallbacks
{
    [SerializeField]
    protected int _id;
    public bool triggered = false;


    private void Update()
    {
        //if (photonView.IsMine)
        //{
        //    // TODO: check for trap activation input
        //    if (true) /* trap activated */
        //    {
        //        photonView.RPC("ActivateTrap", RpcTarget.All);
        //    }
        //}
    }

    public int GetID()
    {
        return _id;
    }

    public bool GetTriggered()
    {
        return triggered;
    }
    public void SetID(int id)
    {
        _id = id;
    }

    public void SetTriggered(bool isTriggered)
    {
        triggered = isTriggered;
    }

    public abstract void ActivateTrap();
}
