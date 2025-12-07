using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 MoveInput => m_moveInput;
        public bool IsJump => m_isJump;

        private InputSystemActions m_moveAction;

        private Vector2 m_moveInput;
        private bool m_isJump;

        private void Awake()
        {
            m_moveAction = new InputSystemActions();
        }

        private void OnEnable()
        {
            m_moveAction?.Player.Enable();
            m_moveAction.Player.Move.performed += OnMovePerformed;
            m_moveAction.Player.Move.canceled += OnMoveCanceled;

            m_moveAction.Player.Jump.performed += OnJumpPerformed;
            m_moveAction.Player.Jump.canceled += OnJumpCanceled;
        }

        private void OnDisable()
        {
            m_moveAction?.Player.Disable();
            m_moveAction.Player.Move.performed -= OnMovePerformed;
            m_moveAction.Player.Move.canceled -= OnMoveCanceled;

            m_moveAction.Player.Jump.performed -= OnJumpPerformed;
            m_moveAction.Player.Jump.canceled -= OnJumpCanceled;
        }

        private void OnMovePerformed(InputAction.CallbackContext contex) =>
            m_moveInput = contex.ReadValue<Vector2>();


        private void OnMoveCanceled(InputAction.CallbackContext context) =>
            m_moveInput = Vector2.zero;

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            m_isJump = true;
            Debug.Log("Jump P");
        }
            

        private void OnJumpCanceled(InputAction.CallbackContext context)
        {
            m_isJump = false;
            Debug.Log("Jump C");
        } 
    }
}