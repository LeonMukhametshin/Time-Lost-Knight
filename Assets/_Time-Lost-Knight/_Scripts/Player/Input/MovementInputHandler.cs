using UnityEngine;

public class MovementInputHandler 
{
    private readonly GameInput m_input;
    private readonly IControllable m_controllable;

    public MovementInputHandler(GameInput input, IControllable controllable)
    {
        m_input = input;
        m_controllable = controllable;

        m_input.Player.Jump.performed += _ => m_controllable.Jump();
        m_input.Player.Dash.performed += _ => m_controllable.Dash();
    }

    public void Update()
    {
        Vector2 input = m_input.Player.Move.ReadValue<Vector2>();
        m_controllable.Move(input);
    }
}