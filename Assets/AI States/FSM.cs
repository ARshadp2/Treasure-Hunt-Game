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

    public TempHealth tempHealth;
    void Start()
    {
        currentState = State.Patrol; // Start in patrol mode
        SelectNextWaypoint();
    }

    void Update()
    {

        if (tempHealth.health <= lowHealthThreshold && currentState != State.Flee)
            {
                currentState = State.Flee;
            }



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
    }




    // Patrol behavior
    void Patrol()
    {
        if (health < lowHealthThreshold)
        {
            currentState = State.Flee; // Transition to flee if health is low
            return;
        }

        MoveTowardsTarget();

    
        if (Vector3.Distance(transform.position, targetPosition) < detectionDistance)
        {
            SelectNextWaypoint();
        }


        if (Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            currentState = State.Chase;
        }
    }

    // Chase behavior
    void Chase()
    {
        if (health < lowHealthThreshold)
        {
            currentState = State.Flee; // Transition to flee if health is low
            return;
        }

        if (player != null)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
        if (health < lowHealthThreshold)
        {
            currentState = State.Flee; // Transition to flee if health is low
            return;
        }

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time;
        }

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


    //Flee
   void Flee()
    {
        Debug.Log("AI is in Flee state");

        MoveAwayFromPlayer();
        
        if (health >= lowHealthThreshold && Vector3.Distance(transform.position, player.position) > chaseRange)
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
            
                rb.isKinematic = false;

                
                Vector3 direction = (player.position - transform.position).normalized;
                
            
                Quaternion rotation = Quaternion.LookRotation(direction);
                projectile.transform.rotation = rotation;

            
                rb.velocity = direction * projectileSpeed;

            
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

        Debug.Log("Moving away from player");
    }

   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange); 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);  
    }
}
