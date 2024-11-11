using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public RunState runState; // Reference to the state for running away from the player
    public int maxProjectiles = 5;
    private static int count = 0;
    public float attackRange = 5f;
    private float noAttackRange = 2f;
    public Transform player;
    public GameObject projectilePrefab;
    public static float projectileSpeed = 10f;
    public float damage = 1f;
    private float health = 10f;

    public ChaseState chaseState; 

    public override State RunCurrentState()
    {
        if (health < 3)
        {
            return runState;  
        }
        else
        {
            Attack();  
            return this;
        }
    }

    void Attack()
    {
        transform.LookAt(player); 

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRange && distanceToPlayer > noAttackRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, attackRange))
            {
                if (hit.transform == player && count < maxProjectiles)
                {
                    count++;
                    AttackPlayer(); 
                }
            }
        }
    }

    void AttackPlayer()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = (player.position - transform.position).normalized * projectileSpeed;
        }
    }
}
