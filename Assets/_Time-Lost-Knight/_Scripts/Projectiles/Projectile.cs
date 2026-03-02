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

    private CircleCollider2D m_hitTrigger;

    private void Awake()
    {
        EnsureTriggerCollider();
    }

    private void Start()
    {
        m_projectileRigidboby.gravityScale = 0f;
        m_projectileRigidboby.linearVelocity = transform.right * m_data.speed;
        m_isGravityOn = false;
        m_xStartPosition = transform.position.x;
    }

    public void Initialize(RangeAttackData data)
    {
        if (m_data is not null)
        {
            return;
        }

        m_data = data;
    }

    private void Update()
    {
        if (!m_hasHitGround && m_isGravityOn)
        {
            float angle = Mathf.Atan2(m_projectileRigidboby.linearVelocityY, m_projectileRigidboby.linearVelocityX) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void FixedUpdate()
    {
        if (m_hasHitGround)
        {
            return;
        }

        if (Mathf.Abs(m_xStartPosition - transform.position.x) >= m_data.trevelDistance && !m_isGravityOn)
        {
            m_isGravityOn = true;
            m_projectileRigidboby.gravityScale = m_gravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryHandleTrigger(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryHandleTrigger(other);
    }
    private void TryHandleTrigger(Collider2D other)
    {
        if (m_hasHitGround)
        {
            return;
        }

        int otherLayer = other.gameObject.layer;
        if (IsLayerInMask(otherLayer, m_playerLayer) && TryGetDamageable(other, out var damageable))
        {
            damageable.TakeDamage(m_data.damage);
            Destroy(gameObject);
            return;
        }

        if (IsLayerInMask(otherLayer, m_grondLayer))
        {
            m_hasHitGround = true;
            m_projectileRigidboby.gravityScale = 0f;
            m_projectileRigidboby.linearVelocity = Vector2.zero;
            StopProjectile();
        }
    }

    private void EnsureTriggerCollider()
    {
        if (!TryGetComponent(out m_hitTrigger))
        {
            m_hitTrigger = gameObject.AddComponent<CircleCollider2D>();
        }

        m_hitTrigger.isTrigger = true;
        m_hitTrigger.radius = m_damageRadius;

        Vector3 damagePosition = m_damagePosition is null ? transform.position : m_damagePosition.position;
        Vector2 localDamagePosition = transform.InverseTransformPoint(damagePosition);
        m_hitTrigger.offset = localDamagePosition;
    }

    private static bool IsLayerInMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }

    private void StopProjectile()
    {
        m_hasHitGround = true;
        m_projectileRigidboby.gravityScale = 0f;
        m_projectileRigidboby.linearVelocity = Vector2.zero;
    }

    private static bool TryGetDamageable(Component target, out IDamageable damageable)
    {
        if (target.TryGetComponent(out damageable))
        {
            return true;
        }

        damageable = target.GetComponentInParent<IDamageable>();
        return damageable is not null;
    }
}