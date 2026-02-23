using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    public event Action pausePressed;

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            pausePressed?.Invoke();
        }
    }
}