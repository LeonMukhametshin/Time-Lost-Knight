using UnityEngine;

public class Movement : CoreComponent, IUpdate
{
    [field: SerializeField] public Rigidbody2D rigidbody2D { get; private set; }
    public Vector2 currentVelocity { get; private set; }
    public bool canSetVelocity { get; set; } = true;

    private Vector2 m_workspace;

    public override void Awake()
    {
        base.Awake();

        core.AddUpdateComponent(this);
    }

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
        m_workspace.Set(velocity, currentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        m_workspace.Set(currentVelocity.x, velocity);
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