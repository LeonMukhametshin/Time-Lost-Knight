using System;
using UnityEngine;

public partial class GameplayEntryPoint : MonoBehaviour
{
    public event Action goToMainMenuSceneRequested;

    [SerializeField] private UIGameplayRootBinder m_sceneUIRoot;

    [SerializeField] private BootstrapState m_bootstrapState;
    [SerializeField] private CameraManager m_cameraManager;

    public void Run()
    {
        m_sceneUIRoot.GoToGameplayButtonClicked += () =>
        {
            goToMainMenuSceneRequested?.Invoke();
        };

        var fsm = new StateMachine();
        m_bootstrapState.Initialize(fsm);

        fsm.Initialize(
            m_bootstrapState,
            new GameplayState(fsm, m_cameraManager),
            new PauseState(fsm));

        fsm.ChangeState<BootstrapState>();
    }
}