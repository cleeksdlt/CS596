using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour {

    private float moveSpeed = 10.0f;

    [SerializeField]
    private float turnSpeed = 3.0f;

    [SerializeField]
    private float targetPointTolerance = 5.0f;

    [SerializeField]
    private Transform boid;

    private Vector3 initialPosition;
    private Vector3 nextMovementPoint;
    private Vector3 targetPosition;

    private bool LazyFlightBehavior;
    private bool nextTargetLoc = true;

    private void Awake()
    {
        initialPosition = transform.position;
        CalculateNextMovementPoint();

    }

    private void Update()
    {
        if(LazyFlightBehavior == false) {
            moveSpeed = 10.0f;
            nextTargetLoc = true;
            transform.localScale = new Vector3(2, 2, 2);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), turnSpeed * Time.deltaTime);
            if (Vector3.Distance(nextMovementPoint, transform.position) <= targetPointTolerance)
            {
                Vector3.Distance(nextMovementPoint, transform.position);
                CalculateNextMovementPoint();
            }
        } else {
            if(nextTargetLoc) 
            {
                transform.localScale = new Vector3(0, 0, 0);
                moveSpeed = 30.0f;
                CalculateNextMovementPoint();
                nextTargetLoc = false;
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position), turnSpeed * Time.deltaTime);
            if ((boid.GetChild(0).position.x+2.5f) >= transform.position.x)
            {
                nextTargetLoc = true;
            }
        }

    }

    private void CalculateNextMovementPoint()
    {
        float posX = Random.Range(-75, 100);
        float posY = Random.Range(10, 30);
        float posZ = Random.Range(-75, 100);
        targetPosition.x = posX;
        targetPosition.y = posY;
        targetPosition.z = posZ;
        nextMovementPoint = targetPosition;
    }

    public void lazyFlight() 
    {
        LazyFlightBehavior = true;
    }

    public void followTheLeader() 
    {
        LazyFlightBehavior = false;
    }

}
