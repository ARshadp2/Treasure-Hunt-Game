using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public ChaseState chaseState;
    public Transform player; // Reference to the player
    public List<Transform> waypoints;
    public float speed = 2f;
    public float detectionDistance = 10f; 
    public float rotationSpeed = 2f;
    public float viewAngle = 45f;

    private int currentWaypoint = 0;
    private Vector3 targetPosition;
    private bool seePlayer = false; // Track if the player is detected

    public override State RunCurrentState()
    {
        DetectPlayer();

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

        // Raycast to detect walls or obstacles
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, forward, out hit, detectionDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                SelectNewWaypoint();
            }
        }

        // Debug log to track the AI's patrol progress
        Debug.Log($"Current Position: {transform.position}, Target Position: {targetPosition}");

        // If the AI reaches the current waypoint, select the next one
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            SelectNextWaypoint();
        }
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionDistance)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < viewAngle / 2f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionDistance))
                {
                    if (hit.transform == player)
                    {
                        seePlayer = true;
                        Debug.Log("Player detected!");
                        return;
                    }
                }
            }
        }
        seePlayer = false;
        Debug.Log("Player not detected.");
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the target position
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void SelectNextWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        targetPosition = waypoints[currentWaypoint].position;

        Debug.Log("Next waypoint selected: " + targetPosition);
    }

    void SelectNewWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        int newWaypoint = currentWaypoint;
        while (newWaypoint == currentWaypoint)
        {
            newWaypoint = Random.Range(0, waypoints.Count);
        }
        currentWaypoint = newWaypoint;
        targetPosition = waypoints[currentWaypoint].position;

        Debug.Log("New waypoint selected: " + targetPosition);
    }
}
