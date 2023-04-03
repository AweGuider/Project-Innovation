using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from Photon server: " + cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        SceneManager.LoadScene("LobbyTest");
        //PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions { MaxPlayers = 4 }, null);
        //rm = GetComponent<RoomManager>();
    }
}
