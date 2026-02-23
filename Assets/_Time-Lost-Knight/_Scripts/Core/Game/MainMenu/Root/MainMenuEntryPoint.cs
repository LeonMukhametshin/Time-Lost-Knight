using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    public event Action GoToGameplaySceneRequested;

    [SerializeField] private UIMainMenuRootBinder m_sceneUIRoot;

    public void Run()
    {
        m_sceneUIRoot.GoToMainMenuButtonClicked += () =>
        {
            GoToGameplaySceneRequested?.Invoke();
        };
    }
}