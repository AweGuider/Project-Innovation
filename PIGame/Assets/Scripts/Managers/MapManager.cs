using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float spawnRadius;
    [SerializeField] private bool adjustSpawnRadius;

#if PC

#elif PHONE
    [SerializeField]
    private string _role;
    [SerializeField]
    private int _team;
    [SerializeField]
    private GameObject _team1SpawnPoint;
    [SerializeField]
    private GameObject _team2SpawnPoint;
#endif
    // Check later how to use
    //private bool isGameStarted = false;
    //private int playerIndex = -1;
    //private int[] teamIndices = new int[4];
    //private Dictionary<int, GameObject> players = new Dictionary<int, GameObject>();
    void Start()
    {
        if (!adjustSpawnRadius) spawnRadius = 5;

#if PC

        //PhotonNetwork.Instantiate(playerPrefab.name, playerPrefab.transform.position, Quaternion.identity);

#elif PHONE
        _role = PlayerPrefs.GetString("Role");
        _team = PlayerPrefs.GetInt("Team");

        if (_role == "Toy")
        {
            Vector3 pos = new();
            if (_team == 1)
            {
                pos = _team1SpawnPoint.transform.position;
            }
            else if (_team == 2)
            {
                pos = _team2SpawnPoint.transform.position;

            }
            PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
            // Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
        }
#endif
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
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
}
