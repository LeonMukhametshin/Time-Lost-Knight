public class GameplayState : IState
{
    private StateMachine m_stateMachine;

    public GameplayState(
        StateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }

    public void Enter()
    {
        
    }

    public void Exit() { }
}