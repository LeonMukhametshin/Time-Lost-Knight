using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_projectileRigidboby;
    [SerializeField] private float m_gravity;
    
    [SerializeField] private LayerMask m_grondLayer;
    [SerializeField] private LayerMask m_playerLayer;

    [SerializeField] private Transform m_damagePosition;
    [SerializeField] private float m_damageRadius;

    private RangeAttackData m_data;

    private float m_xStartPosition;

    private bool m_isGravityOn;
    private bool m_hasHitGround;

    private void Start()
    {
        m_projectileRigidboby.gravityScale = 0f;
        m_projectileRigidboby.linearVelocity = transform.right * m_data.speed;
        m_isGravityOn = false;
        m_xStartPosition = transform.position.x;
    }

    public void Initialize(RangeAttackData data)
    {
        if(m_data is not null)
        {
            return;
        }

        m_data = data;
    }

    private void Update()
    {
        if (!m_hasHitGround)
        {
            if (m_isGravityOn)
            {
                float angle = Mathf.Atan2(m_projectileRigidboby.linearVelocityY, m_projectileRigidboby.linearVelocityX) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (m_hasHitGround)
        {
            return;
        }
        var damageHit = Physics2D.OverlapCircle(m_damagePosition.position, m_damageRadius, m_playerLayer);
        var groundHit = Physics2D.OverlapCircle(m_damagePosition.position, m_damageRadius, m_grondLayer);

        if (damageHit is not null && damageHit.TryGetComponent<IDamageable>(out var damageable))
        {
            Debug.Log("Damage " + damageHit.gameObject.name);
            damageable.TakeDamage(m_data.damage);
            Destroy(gameObject);
        }

        if (groundHit is not null)
        {
            m_hasHitGround = true;
            m_projectileRigidboby.gravityScale = 0f;
            m_projectileRigidboby.linearVelocity = Vector2.zero;
        }

        if (Mathf.Abs(m_xStartPosition - transform.position.x) >= m_data.trevelDistance && !m_isGravityOn)
        {
            m_isGravityOn = true;
            m_projectileRigidboby.gravityScale = m_gravity;
        }
    }
}