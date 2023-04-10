using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _player;

    //private 

    [SerializeField]
    private string _role;
    [SerializeField]
    private int _team;

    [SerializeField]
    private TextMeshProUGUI viewIsMine;

    // Start is called before the first frame update
    void Start()
    {
        _role = PlayerPrefs.GetString("Role");
        _team = PlayerPrefs.GetInt("Team");

        photonView.RequestOwnership();
        photonView.RPC("SpawnPlayer", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _role, _team);
        Debug.LogError($"Player Manager ID: {photonView.ViewID}, Local Player's ActorNumber: {PhotonNetwork.LocalPlayer.ActorNumber}");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
        viewIsMine.text = $"{photonView.IsMine}";
    }

    [PunRPC]
    public void SpawnPlayer(PlayerData data)
    {
        _player = Instantiate(data.gameObject, data.gameObject.transform.position, Quaternion.identity);
    }


    [PunRPC]
    public void AssignOwnership(int viewID)
    {
        Debug.LogError($"Assigning Ownership to ID: {viewID}");
        photonView.RequestOwnership();

        PhotonView view = PhotonView.Find(viewID);
        if (view != null)
        {
            view.RequestOwnership();
        }
    }
}
