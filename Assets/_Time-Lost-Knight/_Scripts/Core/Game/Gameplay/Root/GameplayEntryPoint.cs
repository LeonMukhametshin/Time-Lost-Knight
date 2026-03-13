using System;
using UnityEngine;

public partial class GameplayEntryPoint : MonoBehaviour
{
    public event Action goToMainMenuSceneRequested;

    [SerializeField] private UIGameplayRootBinder m_sceneUIRoot;

    [SerializeField] private BootstrapState m_bootstrapState;
    
    [SerializeField] private PlayerHealthBarView m_healthBarView;
    [SerializeField] private CameraManager m_cameraManager;

    [SerializeField] private Pause m_pause;
    [SerializeField] private PauseWindow m_pauseWindow;
    [SerializeField] private EndGameWindow m_endGameWindow;

    [SerializeField] private PlayerInputHandler m_playerInputHandler;
    [SerializeField] private UIInputHandler m_uIInputHandler;
    [SerializeField] private ParticleManager m_particleManager;
    [SerializeField] private InteractPrompt m_interactPrompt;

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
            new GameplayState(fsm, m_cameraManager, 
                m_healthBarView, m_pause, 
                m_pauseWindow, m_playerInputHandler,
                m_uIInputHandler, m_particleManager,
                m_interactPrompt, m_endGameWindow),
            new PauseState(fsm));

        fsm.ChangeState<BootstrapState>();
    }
}