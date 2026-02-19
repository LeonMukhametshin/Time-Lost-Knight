using System;
using UnityEngine;
using Zenject;

public class GameplayEntryPoint : MonoBehaviour
{
    public event Action goToMainMenuSceneRequested;

    [SerializeField] private UIGameplayRootBinder m_sceneUIRootPrefab;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(m_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToGameplayButtonClicked += () =>
        {
            goToMainMenuSceneRequested?.Invoke();
        };

        var fsm = new StateMachine();

        fsm.Initialize(
            new GameplayState(fsm),
            new PauseState(fsm));

        fsm.ChangeState<GameplayState>();
    }
}