using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    public class PlayerInput
    {
        private InputSystemActions m_moveAction;

        public Vector2 MoveInput { get; private set; }
        public bool IsJumpPressed { get; private set; }

        public PlayerInput(InputSystemActions moveAction)
        {
            m_moveAction = moveAction;

            Enable();
        }

        public void Enable()
        {
            m_moveAction?.Player.Enable();

            m_moveAction.Player.Move.performed += OnMovePerformed;
            m_moveAction.Player.Move.canceled += OnMoveCanceled;

            m_moveAction.Player.Jump.performed += OnJumpPerformed;
            m_moveAction.Player.Jump.canceled += OnJumpCanceled;
        }

        public void Disable()
        {
            m_moveAction?.Player.Disable();
            m_moveAction.Player.Move.performed -= OnMovePerformed;
            m_moveAction.Player.Move.canceled -= OnMoveCanceled;

            m_moveAction.Player.Jump.performed -= OnJumpPerformed;
            m_moveAction.Player.Jump.canceled -= OnJumpCanceled;
        }

        private void OnMovePerformed(InputAction.CallbackContext context) =>
           MoveInput = context.ReadValue<Vector2>();

        private void OnMoveCanceled(InputAction.CallbackContext context) =>
            MoveInput = Vector2.zero;


        private void OnJumpPerformed(InputAction.CallbackContext context) =>
            IsJumpPressed = true;
            
        private void OnJumpCanceled(InputAction.CallbackContext context) =>
            IsJumpPressed = false;
    }
}