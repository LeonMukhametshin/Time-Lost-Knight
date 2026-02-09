using System.Collections.Generic;

public class FSM 
{
    public State currentState;

    private List<State> states;

    public void Initialize(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void SetState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
