using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerNetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomManager rm;
    private void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Photon Server");

        SceneManager.LoadScene("LobbyTest");

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from Photon server: " + cause.ToString());
    }

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
}
