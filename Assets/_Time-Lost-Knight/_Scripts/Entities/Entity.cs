using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public FSM fsm;

    public EntityData data;

    public Rigidbody2D rigidbody { get; private set; }
    public Animator animator { get; private set; }
    public GameObject aliveGameObject { get; private set; }

    [SerializeField] private Transform m_wallCheck;
    [SerializeField] private Transform m_ledgeCheck;
    [SerializeField] private Transform m_playerCheck;
    [SerializeField] private Transform m_groundCheck;

    public AnimationToFSM animationToFSM { get; private set; }
    public int facingDirection { get; private set; } = 1;
    public int lastDamageDirection { get; private set; }

    protected bool isDead;
    protected bool isStunned;

    private Vector2 m_velocityWorkspace;
    private float m_currentHealth;
    private float m_currentStunResistance;
    private float m_lastDamageTime;

    public virtual void Start()
    {
        //TODO: remove
        aliveGameObject = transform.Find("Alive").gameObject;
        rigidbody = aliveGameObject.GetComponent<Rigidbody2D>();
        animator = aliveGameObject.GetComponent<Animator>();
        animationToFSM = aliveGameObject.GetComponent<AnimationToFSM>();

        m_currentHealth = data.maxHealth;
        m_currentStunResistance = data.stunResistance;

        fsm = new FSM();
    }

    public virtual void Update()
    {
        fsm.currentState.Update();

        if(Time.time >= m_lastDamageTime + data.stunRecoveryTime)
        {
            ResetStunResistance();
        }
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

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        m_velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rigidbody.linearVelocity = m_velocityWorkspace;
    }

    public virtual bool CheckLedge() =>
        Physics2D.Raycast(m_ledgeCheck.position, Vector2.down,
            data.wallCheckDistance, data.groundLayer);

    public virtual bool CheckWall() =>
        Physics2D.Raycast(m_wallCheck.position, aliveGameObject.transform.right,
            data.wallCheckDistance, data.groundLayer);

    public virtual bool CheckGround() =>
        Physics2D.OverlapCircle(m_groundCheck.position, data.groundCheckRadius, data.groundLayer);

    public virtual bool CheckPlayerInMinAgroRange() =>
        Physics2D.Raycast(m_playerCheck.position, aliveGameObject.transform.right,
            data.minAgroDistance, data.playerLayer);

    public virtual bool CheckPlayerInMaxAgroRange() =>
        Physics2D.Raycast(m_playerCheck.position, aliveGameObject.transform.right,
            data.maxAgroDistance, data.playerLayer);

    public virtual bool CheckPlayerInCloseRangeAction() =>
        Physics2D.Raycast(m_playerCheck.position, aliveGameObject.transform.right, data.closeRangeActionDistance, data.playerLayer);
    
    public void TakeDamage(AttackDetails details)
    {
        Damage(details);
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        m_currentStunResistance = data.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        if(attackDetails.damageAmout < 0)
        {
            throw new ArgumentException("Damage can`t be negative");
        }

        m_lastDamageTime = Time.time;

        m_currentStunResistance -= attackDetails.stunDamageAmount;
        m_currentHealth -= attackDetails.damageAmout;

        DamageHop(data.damageHopSpeed);

        Instantiate(data.hitParticle, aliveGameObject.transform.position,
            Quaternion.Euler(0f , 0f, UnityEngine.Random.Range(0f, 360f)));

        lastDamageDirection = attackDetails.position.x > aliveGameObject.transform.position.x
            ? -1 : 1;

        if(m_currentStunResistance <= 0 )
        {
            isStunned = true;
        }

        if(m_currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void DamageHop(float velocity)
    {
        m_velocityWorkspace.Set(rigidbody.linearVelocityX, velocity);
        rigidbody.linearVelocity = m_velocityWorkspace;
    }


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
            m_ledgeCheck.position + (Vector3)(Vector2.down * data.ledgeCheckDistance));

        Gizmos.DrawWireSphere(m_playerCheck.position + (Vector3)(Vector2.right * data.closeRangeActionDistance),
            0.2f);
        Gizmos.DrawWireSphere(m_playerCheck.position + (Vector3)(Vector2.right * data.minAgroDistance),
            0.2f);
        Gizmos.DrawWireSphere(m_playerCheck.position + (Vector3)(Vector2.right * data.maxAgroDistance),
            0.2f);
    }
}