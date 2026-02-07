using UnityEngine;

public sealed class MovementAbilityCharges 
{
    private float m_jumpCooldown;
    private float m_dashColdown;

    private int m_maxJumps;
    private int m_maxDashes;

    private float m_nextJumpTime;
    private float m_nextDashTime;

    private int m_currentJumps;
    private int m_currentDashes;

    public MovementAbilityCharges(JumpData jumpData, DashData dashData)
    {
        m_maxJumps = jumpData.maxJumps;
        m_maxDashes = dashData.maxDashes;

        m_jumpCooldown = jumpData.cooldown;
        m_dashColdown = dashData.cooldown;  

        ResetJump();
        ResetDash();
    }

    public bool CanJump(bool isGounded)
    {
        if(isGounded && Time.time >= m_nextJumpTime)
        {
            ResetJump();
        }

        return m_currentJumps > 0;
    }
        

    public bool CanDash(bool isGrounded)
    {
        if (isGrounded && Time.time >= m_nextDashTime)
        {
            ResetDash();
        }

        return m_currentDashes > 0;
    }

    public void ConsumeJump()
    {
        if (m_currentJumps <= 0)
        {
            return;
        }

        m_currentJumps--;
        m_nextJumpTime = Time.time + m_jumpCooldown;
    }

    public void ResetJump()
    {
        m_nextJumpTime = Time.time;
        m_currentJumps = m_maxJumps;
    }

    public void ConsumeDash()
    {
        m_currentDashes--;
        m_nextDashTime = Time.time + m_dashColdown;
    }
         
    public void ResetDash()
    {
        m_currentDashes = m_maxDashes;
        m_nextDashTime = Time.time;
    }
}