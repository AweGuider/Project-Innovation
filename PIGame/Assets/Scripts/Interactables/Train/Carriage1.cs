using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage1 : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = -5f;
    float distanceTravelled;
    public float padding = 0f;
    public float height = 1.5f;

    private void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("Tracks").GetComponent<PathCreator>();
    }
    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled + padding);
        transform.position += new Vector3(0, height, 0);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled + padding);
        transform.rotation *= Quaternion.AngleAxis(90, Vector3.up);
    }
}
