using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private PathCreator _pathCreator;

    [SerializeField]
    private float _speed;
    private float _distanceTravelled;
    [SerializeField]
    private float _padding;


    private void Start()
    {
        _speed = -10;
        _pathCreator = GameObject.FindGameObjectWithTag("Tracks").GetComponent<PathCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_speed == 0) return;
        _distanceTravelled += _speed * Time.deltaTime;
        transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled + _padding);
        transform.position += new Vector3(0, 1.75f, 0);
        transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled + _padding);
        transform.rotation *= Quaternion.AngleAxis(90, Vector3.up);
    }
}
