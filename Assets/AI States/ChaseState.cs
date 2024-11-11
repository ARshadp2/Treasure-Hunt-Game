using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public GameObject player;
    public float chaseRange = 5f;
    public float speed = 10f;

    public override State RunCurrentState()
    {
        Debug.Log("In ChaseState");

        if (IsInRange())
        {
            Debug.Log("Attacking player!");
            return attackState;
        }
        else
        {
            MoveTowardsPlayer();
            return this;
        }
    }

    public bool IsInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        return distanceToPlayer <= chaseRange;
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Debugging: Print the direction towards the player
        Debug.Log("Moving towards player: " + player.transform.position);
    }
}
