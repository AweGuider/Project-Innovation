using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();

    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected To Master");

        // Check if the room is full before joining
        if (PhotonNetwork.CountOfPlayers >= 20)
        {
            Debug.Log("The server is full, try again later.");
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("FullServerScene");
            return;
        }

        SceneManager.LoadScene("TestScene");

//#if PC
//        SceneManager.LoadScene("Server Lobby");
//#elif PHONE
//        SceneManager.LoadScene("Player Lobby");
//#endif
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from the server: " + cause.ToString());
        SceneManager.LoadScene("DisconnectScene");
    }
}
