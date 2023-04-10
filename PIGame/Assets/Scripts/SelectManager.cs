using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button playerButton;


    // Start is called before the first frame update
    void Start()
    {
        serverButton.onClick.AddListener(StartMainGame);
        playerButton.onClick.AddListener(StartPlayerGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }

    private void StartMainGame()
    {
        PhotonNetwork.LoadLevel("Server Lobby");
        //SceneManager.LoadScene("Server Lobby");
    }

    private void StartPlayerGame()
    {
        PhotonNetwork.LoadLevel("Player Lobby");

        //SceneManager.LoadScene("Player Lobby");
    }
}