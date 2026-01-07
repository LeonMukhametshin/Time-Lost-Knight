using UnityEngine;

public class JumpAbility : IPlayerAbility
{
    public string key => "Jump";
    public bool isActive => m_isActive;
    public bool isEnabledByDefault => true;

    private bool m_isActive = true;

    private readonly PlayerMovementController m_movement;
    private readonly PlayerData m_data;

    public JumpAbility(
        PlayerMovementController movement,
        PlayerData data)
    {
        m_movement = movement;
        m_data = data;
    }

    public void Activate()
    {
        m_isActive = true;
    }

    public void Deactivate()
    {
        m_isActive = false;
    }

    public void DoJump()
    {
        m_movement.state.lastPressedJumpTime = m_data.jumpInputBufferTime;
    }

    public void Update()
    {
        if (!m_isActive) return;

        if (m_movement.state.lastPressedJumpTime > 0 &&
            m_movement.state.lastOnGroundTime > 0 &&
            !m_movement.state.isJumping)
        {
            TryJump();
        }
    }

    private void TryJump()
    {
        StartState();

        float force = CalculateJumpForce();
        m_movement.rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void StartState()
    {
        var state = m_movement.state;

        state.isJumping = true;
        state.lastPressedJumpTime = 0;
        state.lastOnGroundTime = 0;

        m_movement.rigidbody.sharedMaterial = m_data.JumpMaterial;
    }

    private float CalculateJumpForce()
    {
        float jumpForce = m_data.JumpForce;

        if (m_movement.rigidbody.linearVelocity.y < 0)
        {
            jumpForce -= m_movement.rigidbody.linearVelocity.y;
        }

        return jumpForce;
    }
}