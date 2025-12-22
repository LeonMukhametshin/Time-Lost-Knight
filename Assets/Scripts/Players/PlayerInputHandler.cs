using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action jump;
    public event Action dash;
    public event Action<Vector2> move;

    private GameInput m_gameInput;

    private void Awake()
    {
        m_gameInput = new GameInput();
    }

    private void OnEnable()
    {
        m_gameInput.Enable();

        m_gameInput.Player.Jump.performed += OnJumpPerformed;
        m_gameInput.Player.Dash.performed += OnDashPerformed;
    }

    private void OnDisable()
    {
        m_gameInput.Player.Jump.performed -= OnJumpPerformed;
        m_gameInput.Player.Dash.performed -= OnDashPerformed;

        m_gameInput.Disable();
    }

    private void Update()
    {
        ReadMove();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context) => jump?.Invoke();

    private void OnDashPerformed(InputAction.CallbackContext context) => dash?.Invoke();

    private void ReadMove()
    {
        Vector2 inputDirection = m_gameInput.Player.Move.ReadValue<Vector2>();
        Vector2 direction = new Vector2(inputDirection.x, 0f);
        move?.Invoke(direction);
    }
}