using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button inputButton;

    public void Start()
    {
        inputButton.onClick.AddListener(OnButtonClick);
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        //PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions { MaxPlayers = 4 }, null);
        //rm = GetComponent<RoomManager>();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room list updated");
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name + " " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }

    public void OnButtonClick()
    {
#if PC
        PhotonNetwork.CreateRoom(inputField.text.ToLowerInvariant(), new RoomOptions { MaxPlayers = 5 });
#elif PHONE
        PhotonNetwork.JoinRoom(inputField.text.ToLowerInvariant());
#endif
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
#if PC
        SceneManager.LoadScene("Server Team Select");
#elif PHONE
        PhotonNetwork.LoadLevel("Player Team Select");
#endif
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
    }

}
