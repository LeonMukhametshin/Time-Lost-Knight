using System;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    public event Action GoToMainMenuSceneRequested;

    [SerializeField] private UIGameplayRootBinder m_sceneUIRootPrefab;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(m_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToGameplayButtonClicked += () =>
        {
            GoToMainMenuSceneRequested?.Invoke();
        };
    }
}