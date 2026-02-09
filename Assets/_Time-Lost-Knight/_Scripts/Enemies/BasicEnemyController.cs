using R3.Triggers;
using System;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject m_alive;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private Animator m_animator;
    [SerializeField] private GameObject m_hitParticle;
    [SerializeField] private GameObject m_deadthChunkParticle;
    [SerializeField] private GameObject m_deadthBloodParticle;

    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private Transform m_wallCheck;

    [SerializeField] private LayerMask m_whatIsGround;

    [SerializeField] private float m_movemetSpeed;
    [SerializeField] private float m_maxHealth;
    [SerializeField] private float m_groundCheckDistance;
    [SerializeField] private float m_wallCheckDistance;
    [SerializeField] private float m_knockbackDuration;
    [SerializeField] private Vector2 m_knockbackSpeed;

    private EnemyState m_currentState;

    private Vector2 m_moveDirection;
    private float m_currentHealth;
    private float m_knockbackStartTime;
    private int m_facingDirection = 1;
    private int m_damageDirection;

    private bool m_groundDetected;
    private bool m_wallDetected;

    private void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    private void Update()
    {
        switch (m_currentState)
        {
            case EnemyState.Moving:
                UpdateMovingState();
                break;
            case EnemyState.Knockback:
                UpdateWKnockbackState();
                break;
            case EnemyState.Dead:
                UpdateDeadState();
                break;
        }
    }

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        m_groundDetected = Physics2D.Raycast(m_groundCheck.position, Vector2.down, m_groundCheckDistance, m_whatIsGround);
        m_wallDetected = Physics2D.Raycast(m_wallCheck.position, Vector2.right, m_wallCheckDistance, m_whatIsGround);
   
        if(!m_groundDetected || m_wallDetected)
        {
            Flip();
        }
        else
        {
            m_moveDirection.Set(m_movemetSpeed * m_facingDirection, m_rigidbody.linearVelocity.y);
            m_rigidbody.linearVelocity = m_moveDirection;
        }
    }

    private void ExitMovingState()
    {

    }

    private void EnterKnockbackState()
    {
        m_knockbackStartTime = Time.time;
        m_moveDirection.Set(m_knockbackSpeed.x * m_damageDirection, m_knockbackSpeed.y);
        m_rigidbody.linearVelocity = m_moveDirection;
        m_animator.SetBool("Knockback", true);
    }

    private void UpdateWKnockbackState()
    {
        if(Time.time > m_knockbackStartTime + m_knockbackDuration)
        {
            SwitchState(EnemyState.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        m_animator.SetBool("Knockback", false);
    }

    private void EnterDeadState()
    {
        Instantiate(m_deadthChunkParticle, m_alive.transform.position, m_deadthChunkParticle.transform.rotation, null);
        Instantiate(m_deadthBloodParticle, m_alive.transform.position, m_deadthBloodParticle.transform.rotation, null);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    public void Damage(float[] attackDetails)
    {
        if(attackDetails[0] < 0)
        {
            throw new ArgumentException("Damage can`t be negative");
        }

        Instantiate(m_hitParticle, m_alive.transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f)));

        m_currentHealth -= attackDetails[0];

        m_damageDirection = attackDetails[1] > m_alive.gameObject.transform.position.x
            ? -1 : 1;

        // Hit Particle

        if(m_currentHealth > 0f)
        {
            SwitchState(EnemyState.Knockback);
        }
        else if(m_currentHealth <= 0f)
        {
            SwitchState(EnemyState.Dead);
        }
    }

    private void SwitchState(EnemyState state)
    {
        switch (m_currentState)
        {
            case EnemyState.Moving:
                ExitMovingState();
                break;
            case EnemyState.Knockback:
                ExitKnockbackState();
                break;
            case EnemyState.Dead:
                ExitDeadState();
                break;
        }

        m_currentState = state;

        switch (m_currentState)
        {
            case EnemyState.Moving:
                EnterMovingState();
                break;
            case EnemyState.Knockback:
                EnterKnockbackState();
                break;
            case EnemyState.Dead:
                EnterDeadState();
                break;
        }
    }

    private void Flip()
    {
        m_facingDirection *= -1;
        m_alive.transform.Rotate(0f, 180f, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(m_groundCheck.position, 
            new Vector2(m_groundCheck.position.x, m_groundCheck.position.y - m_groundCheckDistance));

        Gizmos.DrawLine(m_wallCheck.position,
            new Vector2(m_wallCheck.position.x + m_wallCheckDistance, m_wallCheck.position.y));
    }
}
