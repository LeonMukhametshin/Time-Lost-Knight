using UnityEngine;

public class JumpAbility : IPlayerAbility
{
    private readonly PlayerMovement m_playerMovement;

    public bool isActive => m_isActive;
    public bool canExecute => true;
    public bool isEnabledByDefault => true;
    public string key => "Jump";

    private bool m_isActive = true;

    public JumpAbility(PlayerMovement playerMovement)
    {
        this.m_playerMovement = playerMovement;
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
        m_playerMovement.LastPressedJumpTime = m_playerMovement.Data.JumpInputBufferTime;
    }

    public void Update()
    {
        if (!isActive) return;

        if (m_playerMovement.LastPressedJumpTime > 0 &&
        m_playerMovement.LastOnGroundTime > 0 &&
        !m_playerMovement.IsJumping)
        {
            TryJump();
        }
    }

    private void TryJump()
    {
        StartState();

        float force = CalculateJumpForce();
        m_playerMovement.m_rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void StartState()
    {
        m_playerMovement.IsJumping = true;
        m_playerMovement.LastPressedJumpTime = 0;
        m_playerMovement.LastOnGroundTime = 0;

        m_playerMovement.m_rigidbody.sharedMaterial = m_playerMovement.Data.JumpMaterial;
    }

    private float CalculateJumpForce()
    {
        float jumpForce = m_playerMovement.Data.JumpForce;
        if (m_playerMovement.m_rigidbody.linearVelocityY < 0)
        {
            jumpForce -= m_playerMovement.m_rigidbody.linearVelocityY;
        }
        return jumpForce;
    }
}