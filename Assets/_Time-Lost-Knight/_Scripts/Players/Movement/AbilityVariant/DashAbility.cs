using UnityEngine;

public class DashAbility : IPlayerAbility
{
    public bool isEnabledByDefault => m_isEnabled;
    private bool m_isEnabled = true;   

    private readonly PlayerDashData m_dashData;
    private Rigidbody2D m_rigidbody2D;

    public DashAbility(PlayerDashData dashData, Rigidbody2D rigidbody2D)
    {
        m_dashData = dashData;
        m_rigidbody2D = rigidbody2D;
    }

    public void Activate() =>
        m_isEnabled = true;

    public void Deactivate() =>
        m_isEnabled = false;

    public void Do(AbilityContext contex)
    {
        if(!m_isEnabled)
        {
            return;
        }

        PerformDash(contex.xScale);
    }

    private void PerformDash(int direction)
    {
        Vector2 velosity = new Vector2(direction * m_dashData.dashSpeed, 0f);
        m_rigidbody2D.linearVelocity = velosity;
    }
}