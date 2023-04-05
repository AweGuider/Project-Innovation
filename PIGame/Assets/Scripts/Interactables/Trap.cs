using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviourPunCallbacks
{
    public int id;
    public bool triggered = false;

    public int GetID()
    {
        return id;
    }

    public bool GetTriggered()
    {
        return triggered;
    }

    public void SetTriggered(bool isTriggered)
    {
        triggered = isTriggered;
    }

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

    //[PunRPC]
    //private void ActivateTrap()
    //{
    //    // TODO: activate trap
    //}
    //// other methods...
}
