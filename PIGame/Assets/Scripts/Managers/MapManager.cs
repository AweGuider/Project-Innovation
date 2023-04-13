using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviourPunCallbacks
{
    private static int globalId;
    [SerializeField] private GameObject playerPrefab;
    private Dictionary<int, PlayerData> _players;

    [SerializeField]
    private GameObject _team1SpawnPoint;
    [SerializeField]
    private GameObject _team2SpawnPoint;

    void Start()
    {
        _players = new();
        //Vector3 move = new(0, 0, 0);
        //Vector3 outcome = UpdateSensors(move);
        //Debug.Log($"Values tested: ({move.x}, {move.y}, {move.z}). X: {outcome.x}, Y: {outcome.y}\n");

        //move = new(1, 0, 0);
        //outcome = UpdateSensors(move);
        //Debug.Log($"Values tested: ({move.x}, {move.y}, {move.z}). X: {outcome.x}, Y: {outcome.y}\n");

        //move = new(0, 1, 0);
        //outcome = UpdateSensors(move);
        //Debug.Log($"Values tested: ({move.x}, {move.y}, {move.z}). X: {outcome.x}, Y: {outcome.y}\n");

        //move = new(-1, 0, 0);
        //outcome = UpdateSensors(move);
        //Debug.Log($"Values tested: ({move.x}, {move.y}, {move.z}). X: {outcome.x}, Y: {outcome.y}\n");

        //move = new(0, -1, 0);
        //outcome = UpdateSensors(move);
        //Debug.Log($"Values tested: ({move.x}, {move.y}, {move.z}). X: {outcome.x}, Y: {outcome.y}\n");
        //TestAngle();
    }

    private Vector3 UpdateSensors(Vector3 acc)
    {
        // Receive input
        Vector3 acceleration = acc;
        Vector3 gyroscope = Input.gyro.rotationRate;


        float xDeg = Mathf.Asin(acceleration.x) * Mathf.Rad2Deg;
        float yDeg = Mathf.Asin(acceleration.y) * Mathf.Rad2Deg;

        Debug.LogError($"Degrees X: {xDeg}, Y: {yDeg}");

        float xMag = CalculateMagnitude(xDeg);
        float zMag = CalculateMagnitude(yDeg);

        Debug.LogError($"Magnitude X: {xMag}, Y: {zMag}");

        //Vector3 move = new(xMag, 0, zMag);
        Vector3 move = new(acceleration.x, 0, acceleration.y);

        return move;
    }

    private float CalculateMagnitude(float angle)
    {
        float magnitude = 0;
        if (angle < -20)
        {
            magnitude = Mathf.Clamp(angle, -90, -20) / 90;
        }
        else if (angle > 20)
        {
            magnitude = Mathf.Clamp(angle, 20, 90) / 90;
        }
        return magnitude;
    }

    private void TestAngle()
    {
        Vector3 initialOffset = new(0, Mathf.Sin(-60f * Mathf.Deg2Rad), 0);
        Vector3 acceleration = new(0, Mathf.Sin(-130f * Mathf.Deg2Rad), 0);

        Vector3 offset1 = Quaternion.AngleAxis(-60f, Vector3.up) * new Vector3(0, 0, 0);
        Vector3 offset2 = Quaternion.AngleAxis(-130f, Vector3.up) * new Vector3(0, 0, 0);

        Debug.Log($"Offset 1: Y: {offset1.y}");
        Debug.Log($"Offset 2: Y: {offset2.y}");

        //// Calculate angle difference
        //float angle = Mathf.Acos(Vector3.Dot(initialOffset, acceleration) / (initialOffset.magnitude * acceleration.magnitude)) * Mathf.Rad2Deg;
        //float cross = initialOffset.x * acceleration.y - initialOffset.y * acceleration.x;
        //if (cross > 0) angle *= -1;

        //Debug.Log("Angle difference: " + angle);

        //float yDiff = acceleration.y - initialOffset.y;
        //float angle = Mathf.Asin(yDiff) * Mathf.Rad2Deg;
        ////float angle = Vector3.Angle(initialOffset, acceleration);

        //Debug.Log($"Initial offset by Y: {initialOffset.y}");
        //Debug.Log($"Initial acceleration by Y: {acceleration.y}");
        //Debug.Log($"Angle between offsetted and current: {angle}");
        //Debug.Log($"Initial offset by Y normalized: {innitialOffset.normalized.y}");
        //float tempY = 0;
        //Vector3 acceleration = new(0, tempY, 0);
        //Debug.Log($"Input: X: {acceleration.x}, Y: {acceleration.y}, Z: {acceleration.z}");


        //float xDeg = Mathf.Asin(acceleration.x) * Mathf.Rad2Deg;
        //float yDeg = Mathf.Asin(acceleration.y) * Mathf.Rad2Deg;

        //Debug.Log($"Degrees: X: {xDeg}, Y: {yDeg}");

        //yDeg += 90;

        //Debug.Log($"Updated degrees: X: {xDeg}, Y: {yDeg}");

        //float xMag = CalculateMagnitude(xDeg);
        //float zMag = CalculateMagnitude(yDeg);

        //Debug.Log($"Magnitude: X: {xMag}, Y: {zMag}");

        //if (Mathf.Abs(yDeg) == 180 || Mathf.Abs(yDeg) == 360) yDeg = 0;

        //float yRad = Mathf.Sin(yDeg * Mathf.Deg2Rad);

        //Vector3 move = new(xMag, 0, zMag);
        //Debug.Log($"Output: X: {move.x}, Y: {move.y}, Z: {move.z}");

        //move *= 10 * Time.deltaTime;
        //Debug.Log($"MOVE: X: {move.x}, Y: {move.y}, Z: {move.z}");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from Photon server: " + cause.ToString());
    }

    [PunRPC]
    void UpdatePosition(Player player, Vector3 move, Quaternion rotate)
    {
        Debug.LogError($"Updating position of Local Player's ActorNumber: {player.ActorNumber}");

        PlayerData p = _players[player.ActorNumber];
        p.GetComponent<Rigidbody>().AddForce(move, ForceMode.Impulse);
        Debug.Log($"Move X: {move.x}, Z: {move.z}");
        // TODO: Need to test
        p.transform.rotation = rotate;

        // Don't send if dont want to update player on player side
        //photonView.RPC("UpdatePosition", player, p.transform.position);
    }

    [PunRPC]
    void SpawnPlayer(Player player, string role, int team)
    {
        if (role == "Toy")
        {
            Vector3 pos = new(0, 1, 0);

            if (team == 1)
            {
                pos = _team1SpawnPoint.transform.position;
            }
            else if (team == 2)
            {
                pos = _team2SpawnPoint.transform.position;
            }
            //GameObject playerObject = Instantiate(playerPrefab, pos, Quaternion.identity);
            GameObject playerObject = PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
            PlayerData pPlayer = playerObject.GetComponent<PlayerData>();
            PhotonView playerView = playerObject.GetPhotonView();
            _players.Add(player.ActorNumber, pPlayer);
            //_players.Add(globalId, pPlayer);

            Debug.LogError($"Spawned player ID: {playerView.ViewID}, Player's ActorNumber: {player.ActorNumber}");
            Debug.LogError($"Number of player's total: {_players.Count}");
            for (int i = 0; i < _players.Count; i++)
            {
                Debug.LogError($"Actor's {i} number: {_players.Keys.ToList()[i]}");
            }

            //photonView.RPC("SpawnPlayer", player, pPlayer, globalId);
            //globalId++;
            playerView.TransferOwnership(player);
            //photonView.RPC("AssignOwnership", player, playerView.ViewID);
        }



    }

    //public override void OnJoinedRoom()
    //{
    //    base.OnJoinedRoom();
    //    Debug.Log($"Joined the room: {PhotonNetwork.CurrentRoom.Name}");
    //    PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(0, spawnRadius), 0, Random.Range(0, spawnRadius)), Quaternion.identity);
    //}

    //public void JoinTeam(int teamIndex)
    //{
    //    ExitGames.Client.Photon.Hashtable teamProp = new ExitGames.Client.Photon.Hashtable();
    //    teamProp.Add("team", teamIndex);
    //    PhotonNetwork.LocalPlayer.SetCustomProperties(teamProp);
    //    int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
    //    teamIndices[actorNumber - 1] = teamIndex;
    //    if (PhotonNetwork.CurrentRoom.PlayerCount == 4 && !isGameStarted)
    //    {
    //        isGameStarted = true;
    //        photonView.RPC("StartGame", RpcTarget.All);
    //    }
    //}
}
