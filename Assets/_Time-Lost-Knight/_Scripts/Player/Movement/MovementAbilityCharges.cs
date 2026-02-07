using UnityEngine;

public sealed class MovementAbilityCharges 
{
    private float m_jumpCooldown;
    private float m_dashColdown;

    private int m_maxJumps;

    private float m_nextJumpTime;
    private float m_nextDashTime;

    private int m_currentJumps;

    public MovementAbilityCharges(JumpData jumpData, DashData dashData)
    {
        m_maxJumps = jumpData.maxJumps;

        m_jumpCooldown = jumpData.cooldown;
        m_dashColdown = dashData.cooldown;  

        m_nextJumpTime = 0f;
        m_nextDashTime = 0f;

        ResetJump();
    }

    public bool CanJump(bool isGrounded)
    {
        if (!isGrounded)
        {
            return false;
        }

        return Time.time >= m_nextJumpTime && m_currentJumps > 0;
    }

    public bool CanDash() => 
        Time.time >= m_nextDashTime;

    public void ConsumeJump()
    {
        if (m_currentJumps <= 0)
        {
            return;
        }

        m_currentJumps--;
        m_nextJumpTime = Time.time + m_jumpCooldown;
    }

    public void ConsumeDash()
    {
        m_nextDashTime = Time.time + m_dashColdown;
    }

    public void ResetJump()
    {
        m_nextJumpTime = Time.time;
        m_currentJumps = m_maxJumps;
    }

    public void ResetDash()
    {
        m_nextDashTime = Time.time;
    }
}