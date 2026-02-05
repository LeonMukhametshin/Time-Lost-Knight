public sealed class MovementAbilityCharges 
{
    private int m_maxJumps;
    private int m_maxAirDashes;

    private int m_currentJumps;
    private int m_currentAirDashes;

    public MovementAbilityCharges(int jumps, int airDashes)
    {
        m_maxJumps = jumps;
        m_maxAirDashes = airDashes;

        Reset();
    }

    public bool CanJump() => m_currentJumps > 0;
    public bool CanAirDash() => m_currentAirDashes > 0;

    public void ConsumeJump()
    {
        if (m_currentJumps <= 0)
        {
            return;
        }

        m_currentJumps--;
    }

    public void ConsumeDash()
    {
        if(m_currentAirDashes <= 0)
        {
            return;
        }

        m_currentAirDashes--;
    }

    public void Reset()
    {
        m_currentJumps = m_maxJumps;
        m_currentAirDashes = m_maxAirDashes;
    }
}