using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public RunState runState; // Reference to the state for running away from the player
    public int maxProjectiles = 5;
    private int count = 0;  // Non-static count to track the number of projectiles fired per instance
    public float attackRange = 5f;
    private float noAttackRange = 2f;
    public Transform player;
    public GameObject AI;
    public GameObject projectilePrefab;
    public static float projectileSpeed = 10f;
    public float damage = 1f;
    private float health = 10f;
    public float attackCooldown = 1f;  // Cooldown time between projectiles (in seconds)
    private float lastAttackTime = 0f;  // Track the time of the last attack

    public ChaseState chaseState; 

    public override State RunCurrentState()
    {
        // Log to ensure this method is being called every frame
        Debug.Log("Running AttackState");

        // If health is low, switch to RunState
        if (health < 3)
        {
            Debug.Log("Health is low, switching to RunState.");
            return runState;  
        }
        else
        {
            Debug.Log("Health is sufficient, proceeding to attack.");
            Attack();  
            return this;  // Stay in AttackState while attacking
        }
    }

    void Attack()
{
    AI.transform.LookAt(player);  
    float distanceToPlayer = Vector3.Distance(AI.transform.position, player.position);
    Debug.Log("Distance to player: " + distanceToPlayer);
    if (distanceToPlayer < attackRange && distanceToPlayer > noAttackRange)
    {
        Debug.Log("Player is within attack range.");
        Debug.DrawRay(AI.transform.position, (player.position - AI.transform.position).normalized * attackRange, Color.red);

        if (Time.time - lastAttackTime >= attackCooldown && count < maxProjectiles)
        {
            Debug.Log("Attacking player!");
            count++;  
            lastAttackTime = Time.time;  
            AttackPlayer(); 
        }
        else if (count >= maxProjectiles)
        {
            Debug.Log("Max projectiles reached: " + maxProjectiles);
        }
        else
        {
            Debug.Log("Cooldown active. Waiting to attack.");
        }
    }
    else
    {
        Debug.Log("Player is out of attack range.");
    }
}

void AttackPlayer()
{
    float distanceToPlayer = Vector3.Distance(AI.transform.position, player.position);
    if (distanceToPlayer <= attackRange)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = (player.position - transform.position).normalized * projectileSpeed;
        Destroy(projectile, 2f);
    }
}
}

