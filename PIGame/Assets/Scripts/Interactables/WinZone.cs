using Photon.Pun;
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
