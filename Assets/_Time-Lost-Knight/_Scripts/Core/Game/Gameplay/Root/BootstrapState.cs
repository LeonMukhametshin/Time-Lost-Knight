using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapState : MonoBehaviour, IState
{
    [SerializeField] private PlayerInputHandler m_playerInputHandler;
    [SerializeField] private UIInputHandler m_uIInputHandler;
    [SerializeField] private PlayerSpawner m_playerSpawner;
    [SerializeField] private PlayerHealthBarView m_healthBarView;
    [SerializeField] private InteractPrompt m_interactPrompt;

    private StateMachine m_stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }

    public void Enter()
    {
        ServiceLocator.Register(m_playerInputHandler);
        ServiceLocator.Register(m_uIInputHandler);
        ServiceLocator.Register(m_interactPrompt);

        m_stateMachine.ChangeState<GameplayState>();

        LoadLevel();
    }

    public void Exit() { }

    private void LoadLevel()
    {
        //TODO random level selection
        SceneManager.LoadSceneAsync(
            SceneNames.LEVEL_1,
            LoadSceneMode.Additive)
            .completed += _ =>
            {
                var levelSpawner = ResolveLevelPlayerSpawner();
                if(levelSpawner == null)
                {
                    return;
                }

                levelSpawner.Spawn();
                InitializeLevelCamera();
                m_healthBarView?.Initialize();
            };
    }

    private PlayerSpawner ResolveLevelPlayerSpawner()
    {
        var spawners = FindObjectsByType<PlayerSpawner>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None);

        foreach (var spawner in spawners)
        {
            if (spawner.gameObject.scene.name == SceneNames.LEVEL_1)
            {
                return spawner;
            }
        }

        return m_playerSpawner;
    }

    private void InitializeLevelCamera()
    {
        var virtualCamera = ResolveLevelCamera();
        if(virtualCamera == null)
        {
            return;
        }

        var player = ServiceLocator.Get<Player>().transform;
        virtualCamera.Follow = player;

        if(virtualCamera.LookAt == null)
        {
            virtualCamera.LookAt = player;
        }
    }

    private CinemachineCamera ResolveLevelCamera()
    {
        var virtualCameras = FindObjectsByType<CinemachineCamera>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None);

        foreach(var virtualCamera in virtualCameras)
        {
            if(virtualCamera.gameObject.scene.name == SceneNames.LEVEL_1)
            {
                return virtualCamera;
            }
        }

        return null;
    }
}
