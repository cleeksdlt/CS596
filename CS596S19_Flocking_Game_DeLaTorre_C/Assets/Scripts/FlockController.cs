using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FlockController : MonoBehaviour
{
    [SerializeField]
    private int flockSize = 20;

    //Directly affects how fast our boids can move
    [SerializeField]
    private float speedModifier;

    //Weight values 
    [SerializeField]
    private float alignmentWeight;

    [SerializeField]
    private float cohesionWeight;

    [SerializeField]
    private float separationWeight;

    //Weight the effects of the targets delta versus the boid
    //Want boids to follow more closely? Increase value
    [SerializeField]
    private float followWeight;

    [Header("Boid Data")]
    [SerializeField]
    private Boid prefab;

    [SerializeField]
    private float spawnRadius = 3.0f;
    private Vector3 spawnLocation = Vector3.zero;

    [Header("Follow The Leader Target")]
    [SerializeField]
    public Transform target;

    //Used to calculate the average center of the entire flock. Used in calculating cohesion.
    private Vector3 flockCenter;

    //Used to calculate the entire flock's direction. Used in calculating alignment.
    private Vector3 flockDirection;

    //The direction to the flocking target
    private Vector3 targetDirection;

    //Separation value
    private Vector3 separation;

    public List<Boid> flockList = new List<Boid>();

    public float SpeedModifier { get { return speedModifier; } }

    private void Start()
    {
        flockList = new List<Boid>(flockSize);
        for (int i = 0; i < flockSize; i++)
        {
            spawnLocation = Random.insideUnitSphere * spawnRadius + transform.position;
                Boid boid = Instantiate(prefab, spawnLocation, transform.rotation) as Boid;

            boid.transform.parent = transform;
            boid.FlockController = this;
            flockList.Add(boid);
        }
    }

    public Vector3 Flock(Boid boid, Vector3 boidPosition, Vector3 boidDirection)
    {
        flockDirection = Vector3.zero;
        flockCenter = Vector3.zero;
        targetDirection = Vector3.zero;
        separation = Vector3.zero;

        for (int i = 0; i < flockList.Count; ++i)
        {
            Boid neighbor = flockList[i];
            //Check only against neighbors
            if (neighbor != boid)
            {
                //Aggregate the direction of all the boids
                flockDirection += neighbor.Direction;

                //Aggregate the position of all the boids
                flockCenter += neighbor.transform.localPosition;

                //Aggregate the delta to all the boids
                separation += neighbor.transform.localPosition - boidPosition;

                separation *= -1;
            }
        }

        //Alignment. The average direction of all boids
        flockDirection /= flockSize;
        flockDirection = flockDirection.normalized * alignmentWeight;

        //Cohesion. The centroid of the flock
        flockCenter /= flockSize;
        flockCenter = flockCenter.normalized * cohesionWeight;

        //Separation.
        separation /= flockSize;
        separation = separation.normalized * separationWeight;

        //Direction vector to the target of the flock.
         targetDirection = target.localPosition - boidPosition;
         targetDirection = targetDirection * followWeight;


           
        return flockDirection + flockCenter + separation + targetDirection;
    }

    public void BoidSpeedModifier(float newValue)
    {
        speedModifier = newValue;
    }

    public void AlignmentWeight(float newValue) 
    {
        alignmentWeight = newValue;
    }

    public void CohesionWeight(float newValue)
    {
        cohesionWeight = newValue;
    }

    public void SeparationWeight(float newValue)
    {
        separationWeight = newValue;
    }

    public void FollowWeight(float newValue)
    {
        followWeight = newValue;
    }


}
