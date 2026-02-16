using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rigidbody { get; private set; }
    public Vector2 currentVelocity { get; private set; }

    private Vector2 m_workspace;

    public Movement(Rigidbody2D rigidbody)
    {
        this.rigidbody = rigidbody;
    }

    public void Update() =>
        currentVelocity = rigidbody.linearVelocity;

    public void SetDrag(float linearDamping) =>
        rigidbody.linearDamping = linearDamping;

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        m_workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rigidbody.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        m_workspace = direction * velocity;
        rigidbody.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }

    public void SetVelocityZero()
    {
        rigidbody.linearVelocity = Vector2.zero;
        currentVelocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        m_workspace.Set(velocity, rigidbody.linearVelocityY);
        rigidbody.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }

    public void SetVelocityY(float velocity)
    {
        m_workspace.Set(rigidbody.linearVelocityX, velocity);
        rigidbody.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }
}