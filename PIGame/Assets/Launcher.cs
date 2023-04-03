using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public PhotonView playerPrefab;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected To Master");
        PhotonNetwork.JoinOrCreateRoom("Test", null, null);

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log($"Joined the room: {PhotonNetwork.CurrentRoom.Name}");
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10)), Quaternion.identity);
    }
}
