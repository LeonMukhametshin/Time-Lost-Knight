using UnityEngine;

public class JumpAbility : BasePlayerAbility
{
    public override bool CanExecute
    {
        get
        {
            bool isGroundedOrCoyote = m_movement.IsGrounded() || (m_canUseCoyoteTime && m_jumpCount == 0);
            bool hasJumpsLeft = m_jumpCount < m_jumpConfig.MaxJumpsInAir;
            bool cooldownPassed = (Time.time - m_lastJumpTime) >= m_jumpConfig.JumpCooldown;

            return (isGroundedOrCoyote || hasJumpsLeft) && cooldownPassed;
        }
    }

    private JumpAbilityConfig m_jumpConfig;

    private int m_jumpCount;
    private float m_lastGroundedTime;
    private float m_lastJumpTime;
    private bool m_canUseCoyoteTime;

    public void Initialize(ICharacterMovement movement, IPlayerInput input, JumpAbilityConfig config)
    {
        base.Initialize(movement, input);
        m_jumpConfig = config;
    }

    public override void HandleInput()
    {
        if (!m_isActive || !m_input.IsJumpPressed()) return;

        if (CanExecute)
        {
            float jumpForce = m_jumpConfig.JumpForceMultiplier * m_movement.MaxHorizontalSpeed * 2;
            m_movement.Jump(jumpForce);
            m_jumpCount++;
            m_lastJumpTime = Time.time;
            m_input.ClearJumpInput();
        }
    }

    public override void Update()
    {
        if (m_movement.IsGrounded())
        {
            m_lastGroundedTime = Time.time;
            m_jumpCount = 0;
        }

        m_canUseCoyoteTime = (Time.time - m_lastGroundedTime) <= m_jumpConfig.CoyoteTime;
    }
}