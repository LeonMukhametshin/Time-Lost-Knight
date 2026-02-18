using UnityEngine;

public class Entity : MonoBehaviour
{
    public FSM fsm;

    public EntityData data;

    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement m_movement;

    [field: SerializeField] public Core core { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public AnimationToFSM animationToFSM { get; private set; }

    //TODO: remove after tests
    [SerializeField] private Transform m_wallCheck;
    [SerializeField] private Transform m_ledgeCheck;
    [SerializeField] private Transform m_playerCheck;
    [SerializeField] private Transform m_groundCheck;

    public int facingDirection { get; private set; } = 1;
    public int lastDamageDirection { get; private set; }

    protected bool isDead;
    protected bool isStunned;

    private Vector2 m_velocityWorkspace;

    private float m_lastDamageTime;

    public virtual void Awake()
    {
        fsm = new FSM();
    }

    public virtual void Update()
    {
        fsm.currentState.Update();

        animator.SetFloat(EnemyAnimationConst.Y_VELOCITY, movement.rigidbody2D.linearVelocityY);

        if(Time.time >= m_lastDamageTime + data.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate() =>
        fsm.currentState.FixedUpdate();

    public virtual bool CheckLedge() =>
        Physics2D.Raycast(m_ledgeCheck.position, Vector2.down,
            data.wallCheckDistance, data.groundLayer);

    public virtual bool CheckWall() =>
        Physics2D.Raycast(m_wallCheck.position, transform.right,
            data.wallCheckDistance, data.groundLayer);

    public virtual bool CheckGround() =>
        Physics2D.OverlapCircle(m_groundCheck.position, data.groundCheckRadius, data.groundLayer);

    public virtual bool CheckPlayerInMinAgroRange() =>
        Physics2D.Raycast(m_playerCheck.position, transform.right,
            data.minAgroDistance, data.playerLayer);

    public virtual bool CheckPlayerInMaxAgroRange() =>
        Physics2D.Raycast(m_playerCheck.position, transform.right,
            data.maxAgroDistance, data.playerLayer);

    public virtual bool CheckPlayerInCloseRangeAction() =>
        Physics2D.Raycast(m_playerCheck.position, transform.right, data.closeRangeActionDistance, data.playerLayer);

    public virtual void ResetStunResistance()
    {
        isStunned = false;
    }

    //TODO: remove after tests
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(m_wallCheck.position,
            m_wallCheck.position + (Vector3)(Vector2.right * facingDirection * data.wallCheckDistance));
        Gizmos.DrawLine(m_ledgeCheck.position,
            m_ledgeCheck.position + (Vector3)(Vector2.down * data.ledgeCheckDistance));

        Gizmos.DrawWireSphere(m_playerCheck.position + (Vector3)(Vector2.right * data.closeRangeActionDistance),
            0.2f);
        Gizmos.DrawWireSphere(m_playerCheck.position + (Vector3)(Vector2.right * data.minAgroDistance),
            0.2f);
        Gizmos.DrawWireSphere(m_playerCheck.position + (Vector3)(Vector2.right * data.maxAgroDistance),
            0.2f);
    }
}