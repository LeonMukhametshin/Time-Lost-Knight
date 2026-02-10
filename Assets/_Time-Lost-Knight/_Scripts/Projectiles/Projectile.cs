using DG.Tweening;
using System.Transactions;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float m_gravity;
    [SerializeField] private LayerMask m_grondLayer;
    [SerializeField] private LayerMask m_playerLayer;
    [SerializeField] private Transform m_damagePosition;
    [SerializeField] private float m_damageRadius;

    private AttackDetails m_attackDetails;
    private float m_speed;
    private float m_travelDistance;
    private float m_xStartPosition;

    private bool m_isGravityOn;
    private bool m_hasHitGround;

    private bool m_isInitialize;

    private void Start()
    {
        rigidbody.gravityScale = 0f;
        rigidbody.linearVelocity = transform.right * m_speed;

        m_isGravityOn = false;

        m_xStartPosition = transform.position.x;
    }

    public void Initialize(float speed, float travelDistance, float damage)
    {
        if (m_isInitialize)
        {
                return;
        }

        m_speed = speed;
        m_travelDistance = travelDistance;
        m_attackDetails.damageAmout = damage;

        m_isInitialize = true;
    }

    private void Update()
    {
        if (!m_hasHitGround)
        {
            m_attackDetails.position = transform.position;

            if (m_isGravityOn)
            {
                float angle = Mathf.Atan2(rigidbody.linearVelocityY, rigidbody.linearVelocityX) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!m_hasHitGround)
        {
            var damageHit = Physics2D.OverlapCircle(m_damagePosition.position, m_damageRadius, m_playerLayer);
            var groundHit = Physics2D.OverlapCircle(m_damagePosition.position, m_damageRadius, m_grondLayer);

            if (damageHit is not null && damageHit.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(m_attackDetails);
                Destroy(gameObject);
            }

            if (groundHit is not null)
            {
                m_hasHitGround = true;
                rigidbody.gravityScale = 0f;
                rigidbody.linearVelocity = Vector2.zero;
            }

            if (Mathf.Abs(m_xStartPosition - transform.position.x) >= m_travelDistance && !m_isGravityOn)
            {
                m_isGravityOn = true;
                rigidbody.gravityScale = m_gravity;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_damagePosition.position, m_damageRadius);
    }
}