using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createField;
    [SerializeField] private TMP_InputField joinField;

    public void Start()
    {
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        //PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions { MaxPlayers = 4 }, null);
        //rm = GetComponent<RoomManager>();
    }

    //public void CreateRoom(string s)
    //{
    //    PhotonNetwork.CreateRoom(s, new RoomOptions { MaxPlayers = 4 });
    //}

    //public void JoinRoom(string s)
    //{
    //    PhotonNetwork.JoinRoom(s);
    //}

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room list updated");
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name + " " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }

    public void OnClickCreate()
    {
        PhotonNetwork.CreateRoom(createField.text, new RoomOptions { MaxPlayers = 4 });

    }

    public void OnClickJoin()
    {


        Debug.Log("Joining room: " + joinField.text);
        RoomOptions roomOptions = new RoomOptions();
        TypedLobby typedLobby = new TypedLobby("default", LobbyType.Default);

        PhotonNetwork.JoinOrCreateRoom(joinField.text, roomOptions, typedLobby);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("SampleScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
    }

}
