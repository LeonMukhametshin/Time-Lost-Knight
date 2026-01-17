using UnityEngine;

public class JumpAbility : IPlayerAbility
{
    public bool isEnabledByDefault => m_isEnabled;
    private bool m_isEnabled = true;

    private PlayerJumpData m_jumpData;
    private Rigidbody2D m_rigidbody2D;

    public JumpAbility(PlayerJumpData jumpData, Rigidbody2D rigidbody)
    {
        m_jumpData = jumpData;
        m_rigidbody2D = rigidbody;
    }

    public void Activate()
    {
        m_isEnabled = true;
    }

    public void Deactivate()
    {
        m_isEnabled = false;  
    }

    public void Do(AbilityContext contex)
    {
        if(!m_isEnabled)
        {
            return;
        }
        if(!contex.grounded)
        {
            return;
        }

        float force = CalculateJumpForce();
        m_rigidbody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private float CalculateJumpForce()
    {
        float jumpForce = m_jumpData.jumpForce;

        if (m_rigidbody2D.linearVelocity.y < 0)
        {
            jumpForce -= m_rigidbody2D.linearVelocity.y;
        }

        return jumpForce;
    }
}