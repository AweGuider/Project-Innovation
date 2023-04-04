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
#endif

    private Dictionary<int, Player> _players;


    private void Start()
    {
#if PC
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.CurrentRoom.IsVisible = true;
#elif PHONE
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

#if PHONE
    public void SetNickname()
    {

        if (nicknameInput.text.Length > 1)
        {
            PhotonNetwork.NickName = nicknameInput.text;
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
#endif

    public void SendCommandToPlayer(int playerId, string command)
    {
        if (_players.ContainsKey(playerId))
        {
            //PhotonNetwork.SendAll()
            //_players[playerId].Send("ExecuteCommand", command);
        }
    }

    [PunRPC]
    public void ExecuteCommand(string command)
    {
        Debug.Log("Received command: " + command);
    }
}
