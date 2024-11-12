using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public ChaseState chaseState;
    public Transform player;
    public GameObject AI;
    public List<Transform> waypoints;
    public float speed = 2f;
    public float detectionDistance = 5f;
    public float rotationSpeed = 2f;
    public float viewAngle = 45f;

    private int currentWaypoint = 0;
    private Vector3 targetPosition;
    public bool seePlayer = false;

  public override State RunCurrentState()
    {
        if (player == null || AI == null)
        {
            Debug.LogError("Player or AI GameObject is not assigned!");
            return this; 
        }

        seePlayer = DetectPlayer(); 

        if (seePlayer)
        {
            Debug.Log("Player detected, switching to ChaseState.");
            return chaseState;
        }
        else
        {
            Patrol();  
            return this;  
        }
    }





    private void Start()
    {
        if (waypoints.Count > 0)
        {
            targetPosition = waypoints[currentWaypoint].position;
        }
    }

    private void Update()
    {
        RunCurrentState();
    }

    void Patrol()
    {
        MoveTowardsTarget();

        if (Vector3.Distance(AI.transform.position, targetPosition) < 0.5f)
        {
            SelectNextWaypoint();
        }
    }

    bool DetectPlayer()
{
    Vector3 directionToPlayer = (player.position - AI.transform.position).normalized;
    float distanceToPlayer = Vector3.Distance(AI.transform.position, player.position);

    if (distanceToPlayer <= detectionDistance)
    {
        float angle = Vector3.Angle(AI.transform.forward, directionToPlayer);
        if (angle < viewAngle / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(AI.transform.position, directionToPlayer, out hit, detectionDistance))
            {
                if (hit.transform == player)
                {
                    Debug.Log("Player detected!");
                    return true;
                }
            }
        }
    }

    Debug.Log("Player not detected.");
    return false;
}


    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - AI.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        AI.transform.rotation = Quaternion.Slerp(AI.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        AI.transform.position += AI.transform.forward * speed * Time.deltaTime;
    }

    void SelectNextWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        targetPosition = waypoints[currentWaypoint].position;
    }
}
