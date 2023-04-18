using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        PlayerData player = other.GetComponent<PlayerData>();
        if (player != null && player.GetPlatesPressed() == 5)
        {
            PhotonNetwork.LoadLevel("FinalScene");
        }
    }
}
