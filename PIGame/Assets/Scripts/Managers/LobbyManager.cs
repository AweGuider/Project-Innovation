using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField _inputField;
    [SerializeField]
    private Button _inputButton;

    [SerializeField]
    private Toggle _serverToggle;
    [SerializeField]
    private Toggle _playerToggle;

    public void Start()
    {
        _inputButton.onClick.AddListener(OnButtonClick);
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        //Debug.Log("Joined lobby");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //Debug.Log("Room list updated");
        //foreach (RoomInfo room in roomList)
        //{
        //    Debug.Log("Room name: " + room.Name + ". " + room.PlayerCount + "/" + room.MaxPlayers);
        //}
    }

    public void OnButtonClick()
    {
#if PC
        PhotonNetwork.CreateRoom(_inputField.text.ToLowerInvariant(), new RoomOptions { MaxPlayers = 5, BroadcastPropsChangeToAll = true});
#elif PHONE
        PhotonNetwork.JoinRoom(_inputField.text.ToLowerInvariant());
#endif
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);

        PhotonNetwork.LoadLevel("Player Team Select");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        //Debug.LogError("Failed to create room: " + message);

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        //Debug.LogError("Failed to join room: " + message);
    }
}
