using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button _serverButton;
    [SerializeField] private Button _playerButton;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;


    // Start is called before the first frame update
    void Start()
    {
        _serverButton.onClick.AddListener(StartMainGame);
        _playerButton.onClick.AddListener(StartPlayerGame);

        _playButton.onClick.AddListener(OnPlayClicked);
        _quitButton.onClick.AddListener(OnQuitClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }

    private void StartMainGame()
    {
        SceneManager.LoadScene("Server Lobby");
    }

    private void StartPlayerGame()
    {
        SceneManager.LoadScene("Player Lobby");
    }

    private void OnPlayClicked()
    {
#if PC
        SceneManager.LoadScene("Server Lobby");

#elif PHONE
        SceneManager.LoadScene("Player Lobby");

#endif
    }

    private void OnQuitClicked()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
}