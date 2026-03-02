using System;

public class PauseState : IState
{
    private StateMachine m_stateMachine;

    public PauseState(StateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }

    public void Enter()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }
}