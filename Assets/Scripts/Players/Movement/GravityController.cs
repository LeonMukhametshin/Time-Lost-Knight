using UnityEngine;

public class GravityController
{
    private readonly Rigidbody2D m_rb;
    private readonly PlayerData m_data;
    private readonly float m_baseGravity;

    public GravityController(Rigidbody2D rb, PlayerData data)
    {
        m_rb = rb;
        m_data = data;
        m_baseGravity = rb.gravityScale;
    }

    public void Update(bool isGrounded)
    {
        if (isGrounded)
        {
            m_rb.gravityScale = m_baseGravity;
            return;
        }

        m_rb.gravityScale =
            m_rb.linearVelocityY < 0
                ? m_baseGravity * m_data.FallGravityMultiplier
                : m_baseGravity;
    }

    public void ClampFallSpeed()
    {
        if (m_rb.linearVelocityY < m_data.MaxFallSpeed)
        {
            Vector2 velocity = m_rb.linearVelocity;
            velocity.y = m_data.MaxFallSpeed;
            m_rb.linearVelocity = velocity;
        }
    }
}