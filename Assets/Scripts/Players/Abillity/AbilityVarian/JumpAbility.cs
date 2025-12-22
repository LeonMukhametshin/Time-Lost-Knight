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
        m_playerMovement.lastPressedJumpTime = m_playerMovement.data.JumpInputBufferTime;
    }

    public void Update()
    {
        if (!isActive) return;

        if (m_playerMovement.lastPressedJumpTime > 0 &&
        m_playerMovement.lastOnGroundTime > 0 &&
        !m_playerMovement.isJumping)
        {
            TryJump();
        }
    }

    private void TryJump()
    {
        StartState();

        float force = CalculateJumpForce();
        m_playerMovement.rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void StartState()
    {
        m_playerMovement.isJumping = true;
        m_playerMovement.lastPressedJumpTime = 0;
        m_playerMovement.lastOnGroundTime = 0;

        m_playerMovement.rigidbody.sharedMaterial = m_playerMovement.data.JumpMaterial;
    }

    private float CalculateJumpForce()
    {
        float jumpForce = m_playerMovement.data.JumpForce;
        if (m_playerMovement.rigidbody.linearVelocityY < 0)
        {
            jumpForce -= m_playerMovement.rigidbody.linearVelocityY;
        }
        return jumpForce;
    }
}