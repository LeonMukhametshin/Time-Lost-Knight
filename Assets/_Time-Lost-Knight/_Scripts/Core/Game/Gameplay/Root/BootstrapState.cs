using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapState : MonoBehaviour, IState
{
    [SerializeField] private PlayerInputHandler m_playerInputHandler;
    [SerializeField] private UIInputHandler m_uIInputHandler;
    [SerializeField] private PlayerSpawner m_playerSpawner;
    [SerializeField] private PlayerHealthBarView m_healthBarView;

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

        LoadLevel();
    }

    public void Exit() { }

    private void LoadLevel()
    {
        //TODO random level selection
        m_playerSpawner?.Spawn();

        SceneManager.LoadSceneAsync(
            SceneNames.LEVEL_EXAMPLE,
            LoadSceneMode.Additive)
            .completed += _ =>
            {
                m_healthBarView?.Initialized();
            };
    }
}