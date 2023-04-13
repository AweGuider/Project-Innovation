using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleButton : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private bool _used;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private string _role;
    [SerializeField]
    private int _team;
    [SerializeField]
    private string _buttonID;

    [SerializeField] RoomManager manager;

    ExitGames.Client.Photon.Hashtable buttonProperties;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (manager == null) manager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }

        GetComponent<Button>().onClick.AddListener(OnClick);

        _role = gameObject.name;
        _team = int.Parse(transform.parent.name);
        _buttonID = _team + _role;

        buttonProperties = new();

        //_text.text = _role;

        // Update UI for all players in the room
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.TryGetValue($"{_buttonID}Name", out object name) &&
                player.CustomProperties.TryGetValue($"{_buttonID}Used", out object used))
            {
                _text.text = (string)name;
                _used = (int)used == 1;
            }
            else
            {
                if (!IsUsed())
                {
                    SetText(_role);
                    SetUsed(_used);
                    PhotonNetwork.SetPlayerCustomProperties(buttonProperties);
                }

            }
        }
    }

    private void OnClick()
    {
        if (!IsUsed())
        {
            if (manager.chosenButton != null)
            {
                manager.chosenButton.GetComponent<RoleButton>().ResetButton();
            }
            manager.chosenButton = gameObject;

            PlayerPrefs.SetInt("Team", GetTeam());
            PlayerPrefs.SetString("Role", GetRole());
            SetText(PhotonNetwork.NickName);
            SetUsed(true);
            PhotonNetwork.SetPlayerCustomProperties(buttonProperties);

            manager.SetPlayerReady(PhotonNetwork.LocalPlayer, true);
        }
        else
        {
            if (PhotonNetwork.NickName.ToLowerInvariant() == GetText().ToLowerInvariant())
            {
                manager.chosenButton = null;
                ResetButton();
                manager.SetPlayerReady(PhotonNetwork.LocalPlayer, false);

            }
        }
    }

    public void ResetButton()
    {
        SetText(GetRole());
        SetUsed(false);
        PlayerPrefs.DeleteAll();
        PhotonNetwork.SetPlayerCustomProperties(buttonProperties);
    }

    public bool IsUsed()
    {
        return _used;
    }

    public string GetRole()
    {
        return _role;
    }

    public int GetTeam()
    {
        return _team;
    }
    public string GetText()
    {
        return _text.text;
    }

    public void SetUsed(bool u)
    {
        if (u)
        {
            buttonProperties[_buttonID + "Used"] = 1;
        }
        else
        {
            buttonProperties[_buttonID + "Used"] = 0;
        }
    }

    public void SetText(string t)
    {
        buttonProperties[_buttonID + "Name"] = t;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps == null) return;

        if (changedProps.ContainsKey($"{_buttonID}Used") && changedProps.ContainsKey($"{_buttonID}Name"))
        {
            _text.text = (string)changedProps[_buttonID + "Name"];
            _used = (int)changedProps[_buttonID + "Used"] == 1;
            Debug.Log($"'{targetPlayer.NickName}' updated properties of {_buttonID}. Name: {GetText()}, Used: {IsUsed()}");

        }
    }
}
