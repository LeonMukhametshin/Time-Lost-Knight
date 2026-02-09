using UnityEngine;

public class Entity : MonoBehaviour
{
    public FSM fsm;

    public EntityData data;

    public int facingDirection { get; private set; } = 1;

    public Rigidbody2D rigidbody { get; private set; }
    public Animator animator { get; private set; }
    public GameObject aliveGameObject { get; private set; }

    [SerializeField] private Transform m_wallCheck;
    [SerializeField] private Transform m_ledgeCheck;

    private Vector2 m_velocityWorkspace;

    public virtual void Start()
    {
        aliveGameObject = transform.Find("Alive").gameObject;
        rigidbody = aliveGameObject.GetComponent<Rigidbody2D>();
        animator = aliveGameObject.GetComponent<Animator>();

        fsm = new FSM();
    }

    public virtual void Update()
    {
        fsm.currentState.Update();
    }

    public virtual void FixedUpdate()
    {
        fsm.currentState.FixedUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        m_velocityWorkspace.Set(facingDirection * velocity, rigidbody.linearVelocityY);
        rigidbody.linearVelocity = m_velocityWorkspace;
    }

    public virtual bool CheckLedge() =>
        Physics2D.Raycast(m_ledgeCheck.position, Vector2.down,
            data.wallCheckDistance, data.groundLayer);

    public virtual bool CheckWall() =>
        Physics2D.Raycast(m_wallCheck.position, aliveGameObject.transform.right,
            data.wallCheckDistance, data.groundLayer);

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGameObject.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(m_wallCheck.position,
            m_wallCheck.position + (Vector3)(Vector2.right * facingDirection * data.wallCheckDistance));
        Gizmos.DrawLine(m_ledgeCheck.position,
            m_ledgeCheck.position + (Vector3)(Vector2.down * data.groundCheckDistance));
    }
}