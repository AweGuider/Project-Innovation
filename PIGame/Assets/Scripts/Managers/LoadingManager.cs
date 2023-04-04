using Photon.Pun;
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

#if PC
        SceneManager.LoadScene("Server Lobby");
#elif PHONE
        SceneManager.LoadScene("Player Lobby");
#endif

    }
}
