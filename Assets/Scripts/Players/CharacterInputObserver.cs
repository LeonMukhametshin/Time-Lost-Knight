[System.Serializable]
public class CharacterInputObserver
{
    private WalkAbility m_walk;
    private JumpAbility m_jump;
    private DashAbility m_dash;

    private PlayerInputHandler m_input;

    public CharacterInputObserver(WalkAbility walk, JumpAbility jump, DashAbility dash, PlayerInputHandler input)
    {
        m_walk = walk;
        m_jump = jump;
        m_dash = dash;
        m_input = input;
    }

    public void Subscribe()
    {
        m_input.move += m_walk.DoWalk;
        m_input.jump += m_jump.DoJump;
        m_input.dash += m_dash.DoDash;
    }

    public void Dispose()
    {
        m_input.move -= m_walk.DoWalk;
        m_input.jump -= m_jump.DoJump;
        m_input.dash -= m_dash.DoDash;
    }
}