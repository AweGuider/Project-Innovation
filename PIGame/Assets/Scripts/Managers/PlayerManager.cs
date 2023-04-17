using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private MovementController _movementController;
    [SerializeField]
    private TrapController _trapController;

    //private 
    [SerializeField]
    private string _role;
    [SerializeField]
    private int _team;

    // Start is called before the first frame update
    void Start()
    {
        _role = PlayerPrefs.GetString("Role");
        _team = PlayerPrefs.GetInt("Team");

        //_role = "Kid";
        if (_role == "Toy")
        {
            try
            {
                if (_movementController == null) _movementController = GetComponent<MovementController>();
            }
            catch (Exception e)
            {
                Debug.LogError($"Couldn't find Movement Controller: {e.Message}");
            }
            try
            {
                photonView.RequestOwnership();

                photonView.RPC("SpawnPlayer", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _role, _team);
            }
            catch (Exception e)
            {
                Debug.LogError($"Couldn't spawn a player: {e.Message}");
            }
            Debug.LogError($"Player Manager ID: {photonView.ViewID}, Local Player's ActorNumber: {PhotonNetwork.LocalPlayer.ActorNumber}");
        }
        else if (_role == "Kid")
        {
            try
            {

                if (_trapController == null) _trapController = GetComponent<TrapController>();
            }
            catch (Exception e)
            {
                Debug.LogError($"Couldn't find Trap Controller: {e.Message}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }

    [PunRPC]
    public void AssignOwnership(int viewID)
    {
        Debug.LogError($"Assigning Ownership to ID: {viewID}");
        //photonView.RequestOwnership();
        PhotonView view = PhotonView.Find(viewID);
        if (view != null)
        {
            view.RequestOwnership();
        }
    }

}
