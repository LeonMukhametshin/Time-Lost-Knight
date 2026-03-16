using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    public event Action GoToGameplaySceneRequested;

    [SerializeField] private UIMainMenuRootBinder m_sceneUIRoot;
    private bool m_isInitialized;

    public void Run()
    {
        if (m_isInitialized)
        {
            return;
        }

        if (m_sceneUIRoot == null)
        {
            Debug.LogError("MainMenuEntryPoint has missing references", this);
            return;
        }

        m_isInitialized = true;

        m_sceneUIRoot.GoToMainMenuButtonClicked -= OnGoToGameplayRequested;
        m_sceneUIRoot.GoToMainMenuButtonClicked += OnGoToGameplayRequested;
    }

    private void OnGoToGameplayRequested()
    {
        GoToGameplaySceneRequested?.Invoke();
    }
}
