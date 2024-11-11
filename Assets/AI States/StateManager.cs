using UnityEngine;
public class StateManager : MonoBehaviour
{
    public State currentState;
    public PatrolState patrolState; 

    void Start()
    {
        if (currentState == null)
        {
            currentState = patrolState;
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
                currentState = nextState;
            }
        }
    }
}
