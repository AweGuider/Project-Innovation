using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField nicknameInput;
    [SerializeField]
    private GameObject nameSelectCanvas;
    [SerializeField]
    private GameObject teamSelectCanvas;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    public GameObject chosenButton;
    private Dictionary<Player, bool> _players;

    [SerializeField]
    private Toggle _testingToggle;

    private void Start()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
        //PhotonNetwork.CurrentRoom.IsOpen = true;
        //PhotonNetwork.CurrentRoom.IsVisible = true;
        try
        {
            startButton.onClick.AddListener(StartGame);
            nameSelectCanvas.SetActive(true);
            teamSelectCanvas.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

        _players = new();
    }

    public void SetPlayerReady(Player p, bool r)
    {
        _players[p] = r;
    }

    private int GetReadyPlayers()
    {
        int ready = 0;
        foreach (bool b in _players.Values.ToList())
        {
            if (b) ready++;
        }
        return ready;
    }

    //#if PHONE
    public void SetNickname()
    {
        if (nicknameInput.text.Length > 2)
        {
            PhotonNetwork.NickName = nicknameInput.text;
            Debug.Log($"Nickname is set to: {PhotonNetwork.NickName}");
        }
    }

    public void StartGame()
    {
        Debug.LogWarning($"Are all players ready? {AllPlayersReady()}");
        Debug.LogWarning($"Testing? {Testing()}");
        if (Testing() || AllPlayersReady())
        {
            try
            {
                photonView.RPC("LoadGame", RpcTarget.MasterClient);
            }
            catch (Exception e)
            {
                Debug.LogError($"Couldn't load game map screen: {e.Message}");
            }
        }
        Debug.LogWarning($"Global ready: {GetReadyPlayers()}");
        Debug.LogWarning($"Global - server: {PhotonNetwork.CurrentRoom.MaxPlayers - 1}");

    }

    // Make GREAT check if all players have chosen a role
    private bool AllPlayersReady()
    {
        List<Player> tempPlayers = PhotonNetwork.PlayerList.ToList();
        Debug.LogWarning($"Amount of players: {tempPlayers.Count}");

        if (tempPlayers.Count != PhotonNetwork.CurrentRoom.MaxPlayers) return false;
        Debug.LogWarning($"Amount of max players: {PhotonNetwork.CurrentRoom.MaxPlayers}");

        // TODO: This check doesn't work yet for all READY players
        if (PhotonNetwork.CurrentRoom.MaxPlayers - 1 != GetReadyPlayers()) return false;
        Debug.LogWarning($"Global ready: {GetReadyPlayers()}");

        return true;
    }
//#endif

    public bool Testing()
    {
        return _testingToggle.isOn;
    }

    [PunRPC]
    public void LoadGame()
    {
        SceneManager.LoadScene("Game Map");

        try
        {
            photonView.RPC("LoadPlayerGame", RpcTarget.Others);
        }
        catch (Exception e)
        {
            Debug.LogError($"Couldn't load player screens: {e.Message}");
        }
    }

    [PunRPC]
    public void LoadPlayerGame()
    {
        Debug.Log($"Team: {PlayerPrefs.GetInt("Team")}, Role: {PlayerPrefs.GetString("Role")}");

        Debug.Log($"Loading Game Map");
        if (PlayerPrefs.GetString("Role") == "Kid")
        {
            SceneManager.LoadScene("Kid Screen");
        }
        else
        {
            SceneManager.LoadScene("Toy Screen");
        }
    }
}
