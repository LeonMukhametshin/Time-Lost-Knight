using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    public event Action GoToGameplaySceneRequested;

    [SerializeField] private UIMainMenuRootBinder m_sceneUIRootPrefab;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(m_sceneUIRootPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.GoToMainMenuButtonClicked += () =>
        {
            GoToGameplaySceneRequested?.Invoke();
        };
    }
}