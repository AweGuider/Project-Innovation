using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower2 : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = -10f;
    float distanceTravelled;
    public float padding = 0f;

    private void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("Tracks").GetComponent<PathCreator>();
    }
    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled + padding);
        transform.position += new Vector3(0, 1.75f, 0);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled + padding);
        transform.rotation *= Quaternion.AngleAxis(90, Vector3.up);
    }
}
