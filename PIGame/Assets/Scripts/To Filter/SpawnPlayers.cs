using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(0, 10), playerPrefab.transform.position.y, Random.Range(0, 10));
        PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
    }
}
