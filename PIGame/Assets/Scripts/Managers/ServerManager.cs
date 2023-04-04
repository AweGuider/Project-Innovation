using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [SerializeField] private PhotonView playerPrefab;
    [SerializeField] private float spawnRadius;
    [SerializeField] private bool adjustSpawnRadius;

    // Check later how to use
    //private bool isGameStarted = false;
    //private int playerIndex = -1;
    //private int[] teamIndices = new int[4];
    //private Dictionary<int, GameObject> players = new Dictionary<int, GameObject>();

    //public GameObject toyPrefab;
    //public GameObject kidPrefab;
    //public Transform[] toySpawnPoints;
    //public Transform[] kidSpawnPoints;
    void Start()
    {
        if (!adjustSpawnRadius) spawnRadius = 5;

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


        //PhotonNetwork.JoinOrCreateRoom("Test", null, null);
#if PC
        SceneManager.LoadScene("Server Lobby");

#elif PHONE
        SceneManager.LoadScene("Player Lobby");
#endif
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from Photon server: " + cause.ToString());
    }

    //public override void OnJoinedRoom()
    //{
    //    base.OnJoinedRoom();
    //    Debug.Log($"Joined the room: {PhotonNetwork.CurrentRoom.Name}");
    //    PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(0, spawnRadius), 0, Random.Range(0, spawnRadius)), Quaternion.identity);
    //}

    /// Decide how I want to spawn player
    //[PunRPC]
    //public void SpawnPlayer()
    //{
    //    SpawnPlayerForClient(PhotonNetwork.LocalPlayer);
    //}


    //public void SpawnPlayerForClient(Player player)
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        Vector3 spawnPosition = new Vector3(Random.Range(0, 10), 1, Random.Range(0, 10));
    //        PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Non-master client attempted to spawn player!");
    //    }
    //}

    //public void JoinTeam(int teamIndex)
    //{
    //    ExitGames.Client.Photon.Hashtable teamProp = new ExitGames.Client.Photon.Hashtable();
    //    teamProp.Add("team", teamIndex);
    //    PhotonNetwork.LocalPlayer.SetCustomProperties(teamProp);
    //    int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
    //    teamIndices[actorNumber - 1] = teamIndex;
    //    if (PhotonNetwork.CurrentRoom.PlayerCount == 4 && !isGameStarted)
    //    {
    //        isGameStarted = true;
    //        photonView.RPC("StartGame", RpcTarget.All);
    //    }
    //}

    //public GameObject GetPlayer(int actorNumber)
    //{
    //    return players[actorNumber];
    //}
}
