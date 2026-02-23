using UnityEngine;

public class BootstrapState : MonoBehaviour, IState
{
    [SerializeField] private PlayerInputHandler m_playerInputHandler;
    [SerializeField] private UIInputHandler m_uIInputHandler;

    private StateMachine m_stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }

    public void Enter()
    {
        ServiceLocator.Register(m_playerInputHandler);
        ServiceLocator.Register(m_uIInputHandler);

        m_stateMachine.ChangeState<GameplayState>();
    }

    public void Exit()
    {

    }
}