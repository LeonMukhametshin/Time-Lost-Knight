using System;
using UnityEngine;

public class UIGameplayRootBinder : MonoBehaviour
{
    public event Action GoToGameplayButtonClicked;

    public void HangleGoToMainMenuButtonClick()
    {
        GoToGameplayButtonClicked?.Invoke();
    }

    public void HangleExitGameButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif

        Application.Quit();
    }
}