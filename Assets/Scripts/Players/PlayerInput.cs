using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput => m_moveInput;

    private InputSystem_Actions m_moveAction;
    private Vector2 m_moveInput;

    private void Awake()
    {
        m_moveAction = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        m_moveAction?.Player.Enable();
        m_moveAction.Player.Move.performed += OnMovePerformed;
        m_moveAction.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        m_moveAction?.Player.Disable();
        m_moveAction.Player.Move.performed -= OnMovePerformed;
        m_moveAction.Player.Move.canceled -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext contex) =>
        m_moveInput = contex.ReadValue<Vector2>();
      
    
    private void OnMoveCanceled(InputAction.CallbackContext context) =>
        m_moveInput = Vector2.zero;
}