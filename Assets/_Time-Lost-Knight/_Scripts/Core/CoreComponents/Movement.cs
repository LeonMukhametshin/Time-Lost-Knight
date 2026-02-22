using UnityEngine;

public class Movement : CoreComponent, IUpdate
{
    [field: SerializeField] public Rigidbody2D rigidbody2D { get; private set; }
    public Vector2 currentVelocity { get; private set; }
    public bool canSetVelocity { get; set; } = true;

    private Vector2 m_workspace;

    public void Update() =>
        currentVelocity = rigidbody2D.linearVelocity;

    public void SetDrag(float linearDamping) =>
        rigidbody2D.linearDamping = linearDamping;

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        m_workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rigidbody2D.linearVelocity = m_workspace;
        currentVelocity = m_workspace;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        m_workspace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocityZero()
    {
        m_workspace = Vector2.zero;
        SetFinalVelocity();
    }

    public void SetVelocityX(float velocity)
    {
        m_workspace.Set(velocity, rigidbody2D.linearVelocityY);
        SetFinalVelocity();
    }

    public void SetVelocityXSmooth(float targetVelocityX, float acceleration, float deceleration)
    {
        var rate = Mathf.Abs(targetVelocityX) > 0.01f ? acceleration : deceleration;
        var velocityX = Mathf.Lerp(rigidbody2D.linearVelocityX, targetVelocityX, rate * Time.deltaTime);

        m_workspace.Set(velocityX, rigidbody2D.linearVelocityY);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        m_workspace.Set(rigidbody2D.linearVelocityX, velocity);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (canSetVelocity)
        {
            rigidbody2D.linearVelocity = m_workspace;
            currentVelocity = m_workspace;
        }
    }
}