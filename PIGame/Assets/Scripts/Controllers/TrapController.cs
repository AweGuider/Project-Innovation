using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrapController : MonoBehaviourPunCallbacks
{
    [Header("Door Trap Related")]
    [SerializeField]
    private Button _showDoorsButton;
    [SerializeField]
    private List<GameObject> _doorTraps;

    [Header("Falling Block Trap Related")]
    [SerializeField]
    private Button _showFallingBlockButton;
    [SerializeField]
    private List<GameObject> _fallingBlockTraps;

    [Header("Cash Register Trap Related")]
    [SerializeField]
    private Button _showCashRegistersButton;
    [SerializeField]
    private List<GameObject> _cashRegisterTraps;

    [Header("Train Trap Related")]
    [SerializeField]
    private GameObject _trainTrap;

    private void Start()
    {
        _doorTraps = new();
        _fallingBlockTraps = new();
        _cashRegisterTraps = new();

        _doorTraps = GameObject.FindGameObjectWithTag("Doors")
                            .transform
                            .Cast<Transform>()
                            .Select((transform, index) =>
                            {
                                GameObject go = transform.gameObject;
                                TrapButton tb = go.GetComponent<TrapButton>();
                                tb.SetTrapController(this);
                                tb.SetID(index);
                                return go;
                            })
                            .ToList();

        _fallingBlockTraps = GameObject.FindGameObjectWithTag("FallingBlocks")
                            .transform
                            .Cast<Transform>()
                            .Select((transform, index) =>
                            {
                                GameObject go = transform.gameObject;
                                TrapButton tb = go.GetComponent<TrapButton>();
                                tb.SetTrapController(this);
                                tb.SetID(index);
                                return go;
                            })
                            .ToList();

        _cashRegisterTraps = GameObject.FindGameObjectWithTag("CashRegister")
                            .transform
                            .Cast<Transform>()
                            .Select((transform, index) =>
                            {
                                GameObject go = transform.gameObject;
                                TrapButton tb = go.GetComponent<TrapButton>();
                                tb.SetTrapController(this);
                                tb.SetID(index);
                                return go;
                            })
                            .ToList();

        _trainTrap = GameObject.FindGameObjectWithTag("Train");
        _trainTrap.GetComponent<TrapButton>().SetTrapController(this);
    }

    public void SelectTrap(TrapButton.TrapType type, int id)
    {
        try
        {
            Debug.Log($"Method to be called: 'Select{type}', ID: {id}");
            photonView.RPC($"Select{type}", RpcTarget.MasterClient, id, PlayerPrefs.GetInt("Team"));
        }
        catch (Exception e)
        {
            Debug.LogError($"Couldn't send select trap: {e.Message}");
        }
    }

    public void SelectTrap(TrapButton.TrapType type, bool b)
    {
        try
        {
            Debug.Log($"Method to be called: 'Select{type}', Bool: {b}");
            photonView.RPC($"Select{type}", RpcTarget.MasterClient, b, PlayerPrefs.GetInt("Team"));
        }
        catch (Exception e)
        {
            Debug.LogError($"Couldn't send select trap: {e.Message}");
        }
    }

    public void ActivateTrap(TrapButton.TrapType type, int id)
    {
        try
        {
            Debug.Log($"Method to be called: 'Activate{type}', ID: {id}");
            photonView.RPC($"Activate{type}", RpcTarget.MasterClient, id, PlayerPrefs.GetInt("Team"));
        }
        catch (Exception e)
        {
            Debug.LogError($"Couldn't send activate trap: {e.Message}");
        }
    }

    public void ActivateTrap(TrapButton.TrapType type, bool b)
    {
        try
        {
            Debug.Log($"Method to be called: 'Activate{type}', Bool: {b}");
            photonView.RPC($"Activate{type}", RpcTarget.MasterClient, b, PlayerPrefs.GetInt("Team"));
        }
        catch (Exception e)
        {
            Debug.LogError($"Couldn't send activate trap: {e.Message}");
        }
    }
}
