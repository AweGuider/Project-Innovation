using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage1 : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = -5f;
    float distanceTravelled;

    private void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("Tracks").GetComponent<PathCreator>();

    }
    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        transform.rotation *= Quaternion.AngleAxis(90, Vector3.up);
    }
}
