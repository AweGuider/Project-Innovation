using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class GameController : MonoBehaviourPunCallbacks
{
    private bool isGameStarted = false;
    private int playerIndex = -1;
    private int[] teamIndices = new int[4];
    private Dictionary<int, GameObject> players = new Dictionary<int, GameObject>();

    public GameObject toyPrefab;
    public GameObject kidPrefab;
    public Transform[] toySpawnPoints;
    public Transform[] kidSpawnPoints;

    void Start()
    {
        ConnectToServer();
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions { MaxPlayers = 4 }, null);
    }

    public void JoinTeam(int teamIndex)
    {
        ExitGames.Client.Photon.Hashtable teamProp = new ExitGames.Client.Photon.Hashtable();
        teamProp.Add("team", teamIndex);
        PhotonNetwork.LocalPlayer.SetCustomProperties(teamProp);
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        teamIndices[actorNumber - 1] = teamIndex;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 4 && !isGameStarted)
        {
            isGameStarted = true;
            photonView.RPC("StartGame", RpcTarget.All);
        }
    }

    //[PunRPC]
    //void StartGame()
    //{
    //    Debug.Log("Game started");
    //    SpawnPlayers();
    //}

    //void SpawnPlayers()
    //{
    //    for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
    //    {
    //        int actorNumber = PhotonNetwork.PlayerList[i].ActorNumber;
    //        int teamIndex = (int)PhotonNetwork.PlayerList[i].CustomProperties["team"];
    //        GameObject playerGO;
    //        if (i % 2 == 0)
    //        {
    //            playerGO = PhotonNetwork.Instantiate(toyPrefab.name, toySpawnPoints[i / 2].position, Quaternion.identity);
    //            playerGO.GetComponent<ToyController>().Initialize(actorNumber, teamIndex);
    //        }
    //        else
    //        {
    //            playerGO = PhotonNetwork.Instantiate(kidPrefab.name, kidSpawnPoints[i / 2].position, Quaternion.identity);
    //            playerGO.GetComponent<KidController>().Initialize(actorNumber, teamIndex);
    //        }
    //        players.Add(actorNumber, playerGO);
    //    }
    //}

    public GameObject GetPlayer(int actorNumber)
    {
        return players[actorNumber];
    }
}
