using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject boyKid;
    [SerializeField]
    private GameObject girlKid;
    [SerializeField]
    public Animator boyKidAnimator;
    [SerializeField]
    public Animator girlKidAnimator;

    [SerializeField]
    private GameObject _playerPrefab;
    private Dictionary<int, GameObject> _players;

    [SerializeField]
    private GameObject _team1SpawnPoint;
    [SerializeField]
    private GameObject _team2SpawnPoint;

    [SerializeField]
    private List<GameObject> winDoors;

    void Start()
    {
        _players = new();
        boyKidAnimator = boyKid.transform.GetComponentInChildren<Animator>();
        girlKidAnimator = girlKid.transform.GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        //Debug.Log("Disconnected from Photon server: " + cause.ToString());
    }
    public void OpenDoors()
    {
        foreach (GameObject go in winDoors)
        {
            go.GetComponent<DoorTrap>().ActivateFinal();
        }
    }
    [PunRPC]
    void UpdatePosition(Player player, Vector3 move, Quaternion rotate)
    {
        //Debug.LogError($"Updating position of Local Player's ActorNumber: {player.ActorNumber}");

        GameObject p = _players[player.ActorNumber];
        p.GetComponent<Rigidbody>().AddForce(move, ForceMode.Impulse);

        Debug.Log($"Move X: {move.x}, Z: {move.z}");

        p.transform.rotation = rotate;
    }
    [PunRPC]
    void SpawnPlayer(Player player, string role, int team)
    {
        if (role == "Toy")
        {
            Vector3 pos = new(0, 1, 0);
            string playerName = "";

            if (team == 1)
            {
                pos = _team1SpawnPoint.transform.position;
                playerName = "BunnyPlayer";
            }
            else
            {
                pos = _team2SpawnPoint.transform.position;
                playerName = "ChickenPlayer";

            }

            GameObject playerObject = PhotonNetwork.Instantiate(playerName, pos, Quaternion.identity);
            GameObject pPlayer = playerObject.transform.GetChild(0).gameObject;
            PlayerData pData = pPlayer.GetComponent<PlayerData>();
            pData.SetRole(role);
            pData.SetTeam(team);
            PhotonView playerView = playerObject.GetPhotonView();
            _players.Add(player.ActorNumber, pPlayer);

            playerView.TransferOwnership(player);
            photonView.RPC("SetPlayer", player, playerName + "(Clone)");
        }
    }
}
