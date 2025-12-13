[System.Serializable]
public class CharacterInputObserver
{
    private WalkAbility m_walk;
    private JumpAbility m_jump;
    private DashAbility m_dash;

    private CharacterInputController m_input;

    public CharacterInputObserver(WalkAbility walk, JumpAbility jump, DashAbility dash, CharacterInputController input)
    {
        m_walk = walk;
        m_jump = jump;
        m_dash = dash;
        m_input = input;
    }

    public void Subscribe()
    {
        m_input.Move += m_walk.DoWalk;
        m_input.Jump += m_jump.DoJump;
        m_input.Dash += m_dash.DoDash;
    }

    public void Dispose()
    {
        m_input.Move -= m_walk.DoWalk;
        m_input.Jump -= m_jump.DoJump;
        m_input.Dash -= m_dash.DoDash;
    }
}