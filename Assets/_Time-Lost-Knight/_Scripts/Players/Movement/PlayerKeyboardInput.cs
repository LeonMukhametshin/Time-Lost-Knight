using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeyboardInput : MonoBehaviour
{
    private IControllable m_controllable;
    private GameInput m_gameInput;

    private void Awake()
    {
        m_controllable = GetComponent<IControllable>();
    }

    public void OnEnable()
    {
        m_gameInput = new GameInput();
        m_gameInput.Player.Enable();

        Subscribe();
    }

    public void OnDisable()
    {
        Unsubscribe();

        m_gameInput.Player.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void Subscribe()
    {
        m_gameInput.Player.Jump.performed += OnJump;
        m_gameInput.Player.Dash.performed += OnDash;
    }

    private void Unsubscribe()
    {
        m_gameInput.Player.Jump.performed -= OnJump;
        m_gameInput.Player.Dash.performed -= OnDash;
    }

    private void Move()
    {
        Vector2 input = m_gameInput.Player.Move.ReadValue<Vector2>();
        m_controllable.Move(input);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        m_controllable.Jump();
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        m_controllable.Dash();
    }
}