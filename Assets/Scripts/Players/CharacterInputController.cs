using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputController : MonoBehaviour
{
    public event Action Jump;
    public event Action Dash;
    public event Action<Vector3> Move;

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
    }

    private void Update()
    {
        ReadHorizontalMove();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context) => Jump?.Invoke();

    private void OnDashPerformed(InputAction.CallbackContext context) => Dash?.Invoke();

    private void ReadHorizontalMove()
    {
        var inputDirection = m_gameInput.Player.Move.ReadValue<Vector2>();
        var direction = new Vector3(inputDirection.x, 0f, 0f);

        Move?.Invoke(direction);
    }
}