using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
#if PHONE
    [SerializeField]
    private TMP_InputField nicknameInput;
    [SerializeField]
    private GameObject nameSelectCanvas;
    [SerializeField]
    private GameObject teamSelectCanvas;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TeamSelectionManager teamManager;
    private Dictionary<int, Player> _players;
#endif

    [SerializeField]
    private bool testing;

    private void Start()
    {
#if PC
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.CurrentRoom.IsVisible = true;
#elif PHONE
        startButton.onClick.AddListener(StartGame);
        nameSelectCanvas.SetActive(true);
        teamSelectCanvas.SetActive(false);

        //if (teamController == null) teamController = GameObject.Find("")
        _players = new Dictionary<int, Player>();
#endif
    }

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    Debug.Log("Player entered room: " + newPlayer.NickName);
    //    _players.Add(newPlayer.ActorNumber, newPlayer);
    //    Vector3 spawnPosition = new Vector3(Random.Range(0, 10), 1, Random.Range(0, 10));
    //    PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
    //    //base.OnPlayerEnteredRoom(newPlayer);
    //    //SpawnPlayers();
    //}

    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    Debug.Log("Player left room: " + otherPlayer.NickName);
    //    _players.Remove(otherPlayer.ActorNumber);
    //    //base.OnPlayerLeftRoom(otherPlayer);
    //    // TODO: handle the player leaving the room
    //}

    [PunRPC]
    public void LoadGame()
    {
        // TODO: Check later if needed PhotonNetwork.LoadLevel or SceneManager.LoadScene
#if PC
        PhotonNetwork.LoadLevel("Game Map");
#elif PHONE
        if (PlayerPrefs.GetString("Role") == "Kid")
        {
            SceneManager.LoadScene("Kid Screen");
        }
        else
        {
            SceneManager.LoadScene("Toy Screen");
        }
#endif
    }


#if PHONE
    public void SetNickname()
    {
        if (nicknameInput.text.Length > 1)
        {
            PhotonNetwork.NickName = nicknameInput.text;
            Debug.Log($"Nickname is set to: {PhotonNetwork.NickName}");
        }
    }


    public void StartGame()
    {
        if (testing || AllPlayersReady())
        {
            PlayerPrefs.SetInt("Team", teamManager.GetTeam());
            PlayerPrefs.SetString("Role", teamManager.GetRole());
            Debug.Log($"Team: {PlayerPrefs.GetInt("Team")}, Role: {PlayerPrefs.GetString("Role")}");

            photonView.RPC("LoadGame", RpcTarget.All);
        }
    }

    // Make GREAT check if all players have chosen a role
    private bool AllPlayersReady()
    {
        List<Photon.Realtime.Player> tempPlayers = PhotonNetwork.PlayerList.ToList();
        if (tempPlayers.Count != PhotonNetwork.CurrentRoom.MaxPlayers) return false;
        return true;
    }
#endif

    public void SetTesting(bool b)
    {
        testing = b;
        PhotonNetwork.OfflineMode = testing;

    }

    //public void SendCommandToPlayer(int playerId, string command)
    //{
    //    if (_players.ContainsKey(playerId))
    //    {
    //        //PhotonNetwork.SendAll()
    //        //_players[playerId].Send("ExecuteCommand", command);
    //    }
    //}

    //[PunRPC]
    //public void ExecuteCommand(string command)
    //{
    //    Debug.Log("Received command: " + command);
    //}
}
