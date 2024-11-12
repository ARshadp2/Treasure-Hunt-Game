using UnityEngine;
public class StateManager : MonoBehaviour
{
    public State currentState;
    public PatrolState patrolState;
    public ChaseState chaseState;
    public AttackState attackState;
    public RunState runState;

    void Start()
    {
        if (currentState == null)
        {
            currentState = patrolState;  // Set initial state to patrol
        }
    }

    void Update()
    {
        RunStateMachine();
    }

    void RunStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.RunCurrentState();
            if (nextState != currentState)
            {
                currentState = nextState;  // Transition to next state
            }
        }
    }
}
