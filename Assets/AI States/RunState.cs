using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public PatrolState patrolState;
    public Transform player; 
    public float speed = 3f; 
    public float safeDistance = 30.0f; 
    public float distanceToReturn = 20.0f;
    public override State RunCurrentState()
{
    float distance = Vector3.Distance(transform.position, player.position);
    if (distance > distanceToReturn)
    {
        return patrolState;
    }
    else
    {
        if (distance < safeDistance)
        {
            Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;
            transform.position += directionAwayFromPlayer * speed * Time.deltaTime;
        }
        return this;  // Continue running away from the player
    }
}

}
