public class GameplayState : IState
{
    private StateMachine m_stateMachine;
    private Player m_player;
    private CameraManager cameraManager;

    public GameplayState(
        StateMachine stateMachine, 
        CameraManager cameraManager)
    {
        m_stateMachine = stateMachine;
        this.cameraManager = cameraManager;
    }

    public void Enter()
    {
        var playerPosition = ServiceLocator.Get<PlayerSpawnpoint>();
        ServiceLocator.Get<IPlayerFactorySettings>().position = playerPosition.transform.position;
        m_player = ServiceLocator.Get<PlayerFactory>().Create();
        cameraManager.SetTarget(playerPosition.transform);
    }

    public void Exit() { }
}