using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviourPunCallbacks
{
    [Header("Player Related")]
    [SerializeField]
    private PlayerData _player;
    [SerializeField]
    private GameObject _playerObject;
    [SerializeField]
    private Rigidbody _playerRb;

    [Header("Sensors related")]
    private Vector3 oldAcceleration;
    private Vector3 oldGyroscope;
    private Vector3 initialOffset;
    [SerializeField]
    private float _minPosAngle;
    [SerializeField]
    private float _maxPosAngle;
    [SerializeField]
    private float _minNegAngle;
    [SerializeField]
    private float _maxNegAngle;

    [Header("Boost Related")]
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _speedBoost;
    [SerializeField]
    private float _speedBoostMax;
    [SerializeField]
    private float _boostDuration;
    [SerializeField]
    private float _boostCooldown;
    [SerializeField]
    private bool _isBoosting;
    [SerializeField]
    private Button _boostButton;

    private void Start()
    {
        // General
        _speed = 10f;
        _speedBoost = 1f;
        _speedBoostMax = 1.5f;
        _boostDuration = 3f;
        _boostCooldown = 10f;

        // Phone
        if (_boostButton != null) _boostButton.onClick.AddListener(Boost);

        oldAcceleration = Input.acceleration;
        oldGyroscope = Input.gyro.rotationRate;
        initialOffset = new(0, Mathf.Sin(-60f * Mathf.Deg2Rad), 0);
        //initialOffset = initialOffset.normalized;
        _minPosAngle = 10f;
        _maxPosAngle = 40f;
        _minNegAngle = -10f;
        _maxNegAngle = -40f;
#if PHONE
        Input.gyro.enabled = true;
#endif
    }

    private void FixedUpdate()
    {
#if PC
        if (Input.GetKeyDown(KeyCode.Space) && !_isBoosting)
        {
            Boost();
        }
#endif
        if (_playerObject == null || _playerRb == null)
        {
            //Debug.LogError($"Either player or rigidbody aren't set!");
            return;
        }

        Vector3 move;
#if PC
        move = UpdateKeyboard();
#elif PHONE
        move = UpdateSensors();
#endif
        Vector3 moveDirection = move.normalized;
        Quaternion rotationChange;
        if (moveDirection.magnitude > 0f)
        {
            _player.SetWalking(true);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rotationChange = Quaternion.Slerp(_playerObject.transform.rotation, targetRotation, 5f * Time.deltaTime);
            _playerObject.transform.rotation = rotationChange;
        }
        else
        {
            _player.SetWalking(false);

            rotationChange = _playerObject.transform.rotation;
        }
        moveDirection *= _speed * _speedBoost * Time.deltaTime;

        try
        {
            photonView.RPC("UpdatePosition", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, moveDirection, rotationChange);
        }
        catch (Exception e)
        {
            //Debug.LogError($"Couldn't send updated position: {e.Message}");
        }
    }
    private Vector3 UpdateKeyboard()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return new(x, 0, z);
    }
    private Vector3 UpdateSensors()
    {
        Vector3 acceleration = Input.acceleration;
        Vector3 gyroscope = Input.gyro.rotationRate;

        float xDeg = Mathf.Asin(acceleration.x) * Mathf.Rad2Deg;
        float yDeg = Mathf.Asin(acceleration.y) * Mathf.Rad2Deg;

        float xMag = CalculateMagnitude(xDeg);
        float zMag = CalculateMagnitude(yDeg);

        Vector3 move = new(xMag, 0, zMag);
        //Vector3 move = new(acceleration.x, 0, acceleration.y);

        return move;
    }
    private float CalculateMagnitude(float angle)
    {
        float magnitude = 0;
        if (angle < _minNegAngle)
        {
            magnitude = Mathf.Clamp(angle, _maxNegAngle, _minNegAngle) / _maxPosAngle;
        }
        else if (angle > _minPosAngle)
        {
            magnitude = Mathf.Clamp(angle, _minPosAngle, _maxPosAngle) / _maxPosAngle;
        }
        return magnitude;
    }
    private void Boost()
    {
        StartCoroutine(BoostCoroutine());
    }
    IEnumerator BoostCoroutine()
    {
        _isBoosting = true;
        _speedBoost = _speedBoostMax;
        yield return new WaitForSeconds(_boostDuration);
        _speedBoost = 1f;
        _isBoosting = false;
        yield return new WaitForSeconds(_boostCooldown);
    }
    [PunRPC]
    public void SetPlayer(string name)
    {
        Debug.Log($"GOT HERE!");
        // TODO: Because of this line same character on both player screens
        _playerObject = GameObject.Find(name).transform.GetChild(0).gameObject;
        _playerRb = _playerObject.GetComponent<Rigidbody>();
        _player = _playerObject.GetComponent<PlayerData>();

        if (_player.GetRole() == "Kid")
        {
            _playerObject.SetActive(false);
        }
    }
}
