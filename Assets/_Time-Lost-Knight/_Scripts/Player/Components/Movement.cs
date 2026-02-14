using UnityEngine;

public class Movement 
{
    public Vector2 currentVelocity { get; private set; }

    private Rigidbody2D m_rigidbody;
    private Vector2 m_workspace;

    public Movement(Rigidbody2D rigidbody)
    {
        m_rigidbody = rigidbody;
    }

    public void Update() =>
        currentVelocity = m_rigidbody.linearVelocity;

    public void SetDrag(float linearDamping) =>
        m_rigidbody.linearDamping = linearDamping;

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        m_workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        m_rigidbody.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        m_workspace = direction * velocity;
        m_rigidbody.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }

    public void SetVelocityZero()
    {
        m_rigidbody.linearVelocity = Vector2.zero;
        currentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        m_workspace.Set(velocity, m_rigidbody.linearVelocityY);
        m_rigidbody.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }

    public void SetVelocityY(float velocity)
    {
        m_workspace.Set(m_rigidbody.linearVelocityX, velocity);
        m_rigidbody.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }
}