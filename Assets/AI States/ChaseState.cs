using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class ChaseState : State
{
    public PatrolState patrolState;
    public AttackState attackState;
    public GameObject player;  
    public GameObject AI;      
    public float chaseRange = 5f;
    public float speed = 10f;

    public override State RunCurrentState()
    {
        
        if (!patrolState.seePlayer)
        {
            Debug.Log("Lost sight of player, switching to PatrolState.");
            return patrolState;  
        }

        
        if (IsInRange())
        {
            Debug.Log("Player in attack range, switching to AttackState.");
            return attackState;
        }

        
        MoveTowardsPlayer();
        return this;
    }


    bool IsInRange()
    {
        float distanceToPlayer = Vector3.Distance(AI.transform.position, player.transform.position);
        return distanceToPlayer < chaseRange;
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - AI.transform.position).normalized;
        AI.transform.position += direction * speed * Time.deltaTime;
        AI.transform.LookAt(player.transform.position);
    }
}
