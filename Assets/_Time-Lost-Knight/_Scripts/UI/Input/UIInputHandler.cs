using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

namespace Game.UI.Input
{
    [MovedFrom("")]
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
}
