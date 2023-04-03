using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = -5f;
    float distanceTravelled;
    [SerializeField] private float distanceBetween;
    [SerializeField] private float distanceBetweenLocomotive;
    [SerializeField] private float distanceBetweenCarriage;

    public void Start()
    {
        if (distanceBetweenLocomotive == 0) distanceBetweenLocomotive = 0.5f;
        if (distanceBetweenCarriage == 0) distanceBetweenCarriage = 1.75f;
        pathCreator = GameObject.FindGameObjectWithTag("Tracks").GetComponent<PathCreator>();
        distanceBetween = distanceBetweenLocomotive + (int.Parse(gameObject.name) * distanceBetweenCarriage);
    }

    // Update is called once per frame
    void Update()
    {
        distanceBetween = distanceBetweenLocomotive + (int.Parse(gameObject.name) * distanceBetweenCarriage);

        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled + distanceBetween);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled + distanceBetween);
    }
}
