public class GameplayState : IState
{
    private StateMachine m_stateMachine;
    private Player m_player;
    private CameraManager m_cameraManager;
    private PlayerHealthBarView m_healthBarView;

    public GameplayState(
        StateMachine stateMachine, 
        CameraManager cameraManager,
        PlayerHealthBarView healthBarView)
    {
        m_stateMachine = stateMachine;
        m_cameraManager = cameraManager;
        m_healthBarView = healthBarView;
    }

    public void Enter()
    {
        var playerPosition = ServiceLocator.Get<PlayerSpawnpoint>();
        ServiceLocator.Get<IPlayerFactorySettings>().position = playerPosition.transform.position;
        m_player = ServiceLocator.Get<IPlayerFactory>().Create();

        m_cameraManager.SetTarget(playerPosition.transform);
        m_healthBarView.Initialize(m_player.core.GetCoreComponent<HealthComponent>());
    }

    public void Exit() { }
}