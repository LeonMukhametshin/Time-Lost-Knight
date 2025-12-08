using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    public class PlayerInput
    {
        private InputSystemActions m_moveAction;

        public Vector2 MoveInput { get; private set; }

        public bool JumpPressedThisFrame { get; private set; }
        public bool JumpHeld { get; private set; }
        public bool JumpReleasedThisFrame { get; private set; }

        public bool IsAttacked { get; private set; }

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

            m_moveAction.Player.Attack.performed += OnAttackedPerformed;
            m_moveAction.Player.Attack.canceled += OnAttackedCanceled;
        }

        public void Disable()
        {
            m_moveAction?.Player.Disable();
            m_moveAction.Player.Move.performed -= OnMovePerformed;
            m_moveAction.Player.Move.canceled -= OnMoveCanceled;

            m_moveAction.Player.Jump.performed -= OnJumpPerformed;
            m_moveAction.Player.Jump.canceled -= OnJumpCanceled;

            m_moveAction.Player.Attack.performed -= OnAttackedPerformed;
            m_moveAction.Player.Attack.canceled -= OnAttackedCanceled;
        }

        public void Update()
        {
            ResetFrameInputs();
        }

        private void OnMovePerformed(InputAction.CallbackContext context) =>
           MoveInput = context.ReadValue<Vector2>();

        private void OnMoveCanceled(InputAction.CallbackContext context) =>
            MoveInput = Vector2.zero;

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            JumpPressedThisFrame = true;
            JumpHeld = true;
        }

        private void OnJumpCanceled(InputAction.CallbackContext context)
        {
            JumpHeld = false;
            JumpReleasedThisFrame = true;
        }

        private void OnAttackedPerformed(InputAction.CallbackContext context) =>
            IsAttacked = true;

        private void OnAttackedCanceled(InputAction.CallbackContext context) =>
            IsAttacked = false;

        private void ResetFrameInputs()
        {
            JumpPressedThisFrame = false;
            JumpReleasedThisFrame = false;
        }
    }
}