using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField]
    private GameObject _locomotive;
    [SerializeField]
    private GameObject _cart1;
    [SerializeField]
    private GameObject _cart2;
    [SerializeField]
    private GameObject _cart3;

    [SerializeField]
    private PathCreator _pathCreator;

    [SerializeField]
    private float _speed;
    private float _distanceTravelled;
    [SerializeField]
    private float _padding;

    [SerializeField]
    private bool _isStopped;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _accelerationRate;
    [SerializeField]
    private float _decelerationRate;

    private float _targetSpeed;

    [SerializeField]
    protected bool _selected;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            _pathCreator = GameObject.FindGameObjectWithTag("Tracks").GetComponent<PathCreator>();

            if (_locomotive == null) _locomotive = transform.GetChild(0).gameObject;
            if (_cart1 == null) _cart1 = transform.GetChild(1).gameObject;
            if (_cart2 == null) _cart2 = transform.GetChild(2).gameObject;
            if (_cart3 == null) _cart3 = transform.GetChild(3).gameObject;
        }
        catch (Exception e)
        {
            Debug.LogError($"Couldn't access a gameobject: {e.Message}");
        }

        _speed = -10f;
        _maxSpeed = -10f;
        _targetSpeed = _maxSpeed;
        _cooldown = 15f;
        _isStopped = false;

        _accelerationRate = 5f;
        _decelerationRate = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = _speed;

        if (_isStopped)
        {
            // Decelerate
            _speed = Mathf.MoveTowards(currentSpeed, _targetSpeed, _decelerationRate * Time.deltaTime);
        }
        else
        {
            // Accelerate
            _speed = Mathf.MoveTowards(currentSpeed, _targetSpeed, _accelerationRate * Time.deltaTime);
        }

        _distanceTravelled += _speed * Time.deltaTime;

        // For locomotive
        _locomotive.transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled + _padding);
        _locomotive.transform.position += new Vector3(0, 1.75f, 0);
        _locomotive.transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled + _padding);
        _locomotive.transform.rotation *= Quaternion.AngleAxis(90, Vector3.up);

        // For Cart 1
        _cart1.transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled + _padding + 3.25f);
        _cart1.transform.position += new Vector3(0, 1.5f, 0);
        _cart1.transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled + _padding + 3.25f);
        _cart1.transform.rotation *= Quaternion.AngleAxis(90, Vector3.up);

        // For Cart 2
        _cart2.transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled + _padding + 6.5f);
        _cart2.transform.position += new Vector3(0, 1.25f, 0);
        _cart2.transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled + _padding + 6.5f);
        _cart2.transform.rotation *= Quaternion.AngleAxis(90, Vector3.up);

        // For Cart 3
        _cart3.transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled + _padding + 9.75f);
        _cart3.transform.position += new Vector3(0, 1.25f, 0);
        _cart3.transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled + _padding + 9.75f);
        _cart3.transform.rotation *= Quaternion.AngleAxis(90, Vector3.up);
    }

    public void SelectTrain()
    {
        // TODO: Implement something here
    }

    public void SetStopped(bool s)
    {
        if (s)
        {
            _targetSpeed = 0f; // Set the target speed to zero to decelerate smoothly
            StartCoroutine(TrainCooldown());
        }
        else
        {
            StopAllCoroutines();
            _targetSpeed = _maxSpeed; // Set the target speed back to the max speed to accelerate smoothly
            _isStopped = false;
        }
    }

    IEnumerator TrainCooldown()
    {
        _isStopped = true;
        yield return new WaitForSeconds(_cooldown);
        _targetSpeed = _maxSpeed; // Set the target speed back to the max speed to accelerate smoothly
        _isStopped = false;
    }
}
