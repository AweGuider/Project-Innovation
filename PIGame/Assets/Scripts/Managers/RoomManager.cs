using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
#if PHONE
    [SerializeField]
    private TMP_InputField nicknameInput;
    [SerializeField]
    private GameObject nameSelectCanvas;
    [SerializeField]
    private GameObject teamSelectCanvas;

#endif

    private Dictionary<int, Player> _players;

    [SerializeField]
    private bool testing;


    private void Start()
    {
#if PC
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.CurrentRoom.IsVisible = true;
#elif PHONE

        nameSelectCanvas.SetActive(true);
        teamSelectCanvas.SetActive(false);
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
#if PC
    [PunRPC]
    public void LoadGame()
    {
        PhotonNetwork.LoadLevel("Game Map");
    }
#endif

#if PHONE
    public void SetNickname()
    {
        if (nicknameInput.text.Length > 1)
        {
            PhotonNetwork.NickName = nicknameInput.text;
            Debug.Log($"Nickname is set to: {PhotonNetwork.NickName}");

        }
    }

    // Method to choose team, if needed
    public void OnChooseTeam()
    {

    }

    // Method to choose role, if needed

    public void OnChooseRole()
    {

    }

    public void StartGame()
    {
        if (testing || PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            photonView.RPC("LoadGame", RpcTarget.MasterClient);
        }
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
