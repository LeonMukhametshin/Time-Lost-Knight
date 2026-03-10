using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapState : MonoBehaviour, IState
{
    [SerializeField] private PlayerInputHandler m_playerInputHandler;
    [SerializeField] private PlayerSpawnpoint m_playerSpawner;

    [SerializeField] private Pause m_pause;

    [SerializeField] private UIInputHandler m_uIInputHandler;
    [SerializeField] private InteractPrompt m_interactPrompt;
    [SerializeField] private PauseWindow m_pauseWindow;
    [SerializeField] private ParticleManager m_particleManager;

    private StateMachine m_stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }

    public void Enter()
    {
        var playerFactory = new PlayerFactory("Prefabs/Player");

        ServiceLocator.Register(m_playerSpawner);
        ServiceLocator.Register<IPlayerFactory>(playerFactory);
        ServiceLocator.Register<IPlayerFactorySettings>(playerFactory);

        ServiceLocator.Register(m_playerInputHandler);
        ServiceLocator.Register(m_uIInputHandler);
        ServiceLocator.Register(m_interactPrompt);
        ServiceLocator.Register(m_particleManager);
        ServiceLocator.Register(m_pause);


        m_pauseWindow.Initialize(m_uIInputHandler);

        LoadLevel();
    }

    public void Exit() { }

    private void LoadLevel()
    {
        SceneManager.LoadSceneAsync(
            SceneNames.LEVEL_EXAMPLE,
            LoadSceneMode.Additive)
            .completed += _ =>
            {
                m_stateMachine.ChangeState<GameplayState>();
            };
    }
}