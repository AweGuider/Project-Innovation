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
    [SerializeField]
    private GameObject _playerPrefab;
    private Dictionary<int, GameObject> _players;

    [SerializeField]
    private GameObject _team1SpawnPoint;
    [SerializeField]
    private GameObject _team2SpawnPoint;

    void Start()
    {
        _players = new();
        //AudioManager.instance.PlaySound(AudioManager.AudioType.Music, 0);
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

        GameObject p = _players[player.ActorNumber];
        p.GetComponent<Rigidbody>().AddForce(move, ForceMode.Impulse);

        Debug.Log($"Move X: {move.x}, Z: {move.z}");

        p.transform.rotation = rotate;
    }

    [PunRPC]
    void SpawnPlayer(Player player, string role, int team)
    {
        if (role == "Toy")
        {
            Vector3 pos = new(0, 1, 0);
            string playerName = "";

            if (team == 1)
            {
                pos = _team1SpawnPoint.transform.position;
                playerName = "BunnyPlayer";
            }
            else
            {
                pos = _team2SpawnPoint.transform.position;
                playerName = "ChickenPlayer";

            }

            //GameObject playerObject = PhotonNetwork.Instantiate(_playerPrefab == null ? "Player" : _playerPrefab.name, pos, Quaternion.identity);
            GameObject playerObject = PhotonNetwork.Instantiate(playerName, pos, Quaternion.identity);
            GameObject pPlayer = playerObject.transform.GetChild(0).gameObject;
            PlayerData pData = pPlayer.GetComponent<PlayerData>();
            pData.SetRole(role);
            pData.SetTeam(team);
            PhotonView playerView = playerObject.GetPhotonView();
            _players.Add(player.ActorNumber, pPlayer);

            Debug.LogError($"Spawned player ID: {playerView.ViewID}, Player's ActorNumber: {player.ActorNumber}");
            Debug.LogError($"Number of player's total: {_players.Count}");
            for (int i = 0; i < _players.Count; i++)
            {
                Debug.LogError($"Actor's {i} number: {_players.Keys.ToList()[i]}");
            }

            playerView.TransferOwnership(player);
            photonView.RPC("SetPlayer", player);
            //photonView.RPC("AssignOwnership", player, playerView.ViewID);
        }
    }
}
