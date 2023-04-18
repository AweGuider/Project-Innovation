using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
#if PC
        AudioManager.instance.PlaySound(AudioManager.AudioType.Music, 0, true);
#endif
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();

    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        //Debug.Log("Connected To Master");

        // Check if the room is full before joining
        if (PhotonNetwork.CountOfPlayers >= 20)
        {
            //Debug.Log("The server is full, try again later.");
            PhotonNetwork.Disconnect();
            //SceneManager.LoadScene("FullServerScene");
            return;
        }

        SceneManager.LoadScene("StartScene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //Debug.Log("Disconnected from the server: " + cause.ToString());
        SceneManager.LoadScene("DisconnectScene");
    }
}
