using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action jump;
    public event Action dash;
    public event Action<Vector3> move;

    private GameInput m_gameInput;

    private void Awake()
    {
        m_gameInput = new GameInput();
        m_gameInput.Enable();
    }

    private void OnEnable()
    {
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
        ReadHorizontalMove();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context) => jump?.Invoke();

    private void OnDashPerformed(InputAction.CallbackContext context) => dash?.Invoke();

    private void ReadHorizontalMove()
    {
        var inputDirection = m_gameInput.Player.Move.ReadValue<Vector2>();
        var direction = new Vector3(inputDirection.x, 0f, 0f);

        move?.Invoke(direction);
    }
}