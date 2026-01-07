using System;
using UnityEngine;

public class UIGameplayRootBinder : MonoBehaviour
{
    public event Action GoToGameplayButtonClicked;

    public void HangleGoToMainMenuButtonClick()
    {
        GoToGameplayButtonClicked?.Invoke();
    }
}