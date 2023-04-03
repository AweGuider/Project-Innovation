using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private Dictionary<int, Player> _players;

    private void Awake()
    {
        _players = new Dictionary<int, Player>();
    }

    private void Start()
    {
        if (PhotonNetwork.CurrentRoom == null)
        {
            Debug.LogWarning("Server has not joined a room yet!");
            return;
        }

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.CurrentRoom.IsVisible = true;
        //SpawnPlayers();
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


    private void SpawnPlayers()
    {
        // TODO: spawn players in the room
    }
}
