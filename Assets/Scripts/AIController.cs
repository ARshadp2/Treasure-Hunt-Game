using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    public int maxprojectiles = 5;
    private static int count = 0;
    public float attackRange = 5f;
    private float noattackRange = 2f;
    public Transform player;
    public GameObject projectilePrefab;
    public static float projectileSpeed = 10f;
    public float damage = 1f;
    private float health = 10f;

    void Update()
    {
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) < attackRange && Vector3.Distance(transform.position, player.position) > noattackRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, attackRange))
            {
                if (hit.transform == player && count < maxprojectiles)
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
    }
    public static void lowerCount() {
        count--;
    }
    public static float speed() {
        return projectileSpeed;
    }
    public void Die(float damage)
    {
        if (health <= 0)
        {
            Destroy(gameObject);  // Destroy the AI when health reaches 0
        }
    }
}
