using System;
using UnityEngine;

public class UIMainMenuRootBinder : MonoBehaviour
{
    public event Action GoToMainMenuButtonClicked;

    public void HangleGoToGameplayButtonClick()
    {
        GoToMainMenuButtonClicked?.Invoke();
    }
}