using System;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    public event Action GoToMainMenuButtonClicked;

    public void HangleGoToGameplayButtonClick()
    {
        GoToMainMenuButtonClicked?.Invoke();
    }

    public void HangleExitGameButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif

        Application.Quit();
    }
}