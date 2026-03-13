using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapState : MonoBehaviour, IState
{
    [SerializeField] private PlayerSpawnpoint m_playerSpawner;

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

        LoadLevel();
    }

    public void Exit() { }

    private void LoadLevel()
    {
        SceneManager.LoadSceneAsync(
            SceneNames.LEVEL_EXAMPLE_TWO,
            LoadSceneMode.Additive)
            .completed += _ =>
            {
                m_stateMachine.ChangeState<GameplayState>();
            };
    }
}