using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    // State definitions
    private enum State { Patrol, Chase, Attack, Flee }
    private State currentState;

    // Attack settings
    public float attackRange = 5f;
    public Transform player;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float damage = 1f;
    private float health = 10f;
    public float lowHealthThreshold = 3f; // Threshold for fleeing
    public float attackCooldown = 2f;
    private float lastAttackTime = 0f;



    // Chase settings
    public float chaseRange = 10f;
    public float speed = 3f;

    // Patrol settings
    public List<Transform> waypoints;
    public float detectionDistance = 2f;
    public float rotationSpeed = 2f;

    private int currentWaypoint = 0;
    private Vector3 targetPosition;

    void Start()
    {
        currentState = State.Patrol; // Start in patrol mode
        SelectNextWaypoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Flee:
                Flee();
                break;
        }

        // Check health to switch to Flee if low
        if (health < lowHealthThreshold && currentState != State.Flee)
        {
            currentState = State.Flee;
        }
    }

    // Patrol behavior
    void Patrol()
    {
        MoveTowardsTarget();

        // If close to the waypoint, select the next one
        if (Vector3.Distance(transform.position, targetPosition) < detectionDistance)
        {
            SelectNextWaypoint();
        }

        // Transition to Chase if player is within chase range
        if (Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            currentState = State.Chase;
        }
    }

    // Chase behavior
    void Chase()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // Move towards the player’s position
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Check distance to switch to attack or patrol
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                currentState = State.Attack;
            }
            else if (distanceToPlayer > chaseRange)
            {
                currentState = State.Patrol;
                SelectNextWaypoint();
            }
        }
    }

    // Attack behavior
    void Attack()
    {
        // Check cooldown to avoid firing every frame
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }

        // State transitions
        if (Vector3.Distance(transform.position, player.position) > attackRange && Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            currentState = State.Chase;
        }
        else if (Vector3.Distance(transform.position, player.position) > chaseRange)
        {
            currentState = State.Patrol;
            SelectNextWaypoint();
        }
    }


    // Flee behavior
    void Flee()
    {
        MoveAwayFromPlayer();

        // Return to patrol if far enough from the player and health is safe
        if (Vector3.Distance(transform.position, player.position) > chaseRange && health >= lowHealthThreshold)
        {
            currentState = State.Patrol;
            SelectNextWaypoint();
        }
    }

   void AttackPlayer()
{
    if (projectilePrefab != null)
    {
        // Instantiate projectile at AI's position
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Make sure it's non-kinematic
            rb.isKinematic = false;

            // Calculate direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            
            // Set the projectile's rotation to face the player
            Quaternion rotation = Quaternion.LookRotation(direction);
            projectile.transform.rotation = rotation;

            // Apply velocity in the direction towards the player
            rb.velocity = direction * projectileSpeed;

            // Debugging projectile spawn and movement
            Debug.Log("Projectile spawned at: " + projectile.transform.position);
        }

        Destroy(projectile, 2f); // Destroy after 2 seconds
    }
    else
    {
        Debug.LogWarning("Projectile prefab not assigned.");
    }
}





    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void SelectNextWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        targetPosition = waypoints[currentWaypoint].position;
    }

    void MoveAwayFromPlayer()
    {
        Vector3 direction = (transform.position - player.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // Optional: Visualize Raycast in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); // Visualize attack range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);  // Visualize chase range
    }
}
