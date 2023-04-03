using Photon.Pun;
using UnityEngine;

public class PlayerController2 : MonoBehaviourPunCallbacks, IPunObservable
{
    private Vector3 position;

    private void Update()
    {
        //if (photonView.IsMine)
        //{
        //    // TODO: get input and move the player
        //    position = transform.position;
        //    photonView.RPC("UpdatePosition", RpcTarget.Others, position);
        //}
    }

    //[PunRPC]
    //private void UpdatePosition(Vector3 pos)
    //{
    //    position = pos;
    //}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(position);
        }
        else if (stream.IsReading)
        {
            position = (Vector3)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void ReceiveInput(Vector3 acceleration, Vector3 gyroscope)
    {
        // Do something with the input data, e.g. update the player's movement
    }

}
