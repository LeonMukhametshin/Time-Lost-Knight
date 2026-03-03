using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private float m_gravity = 3f;

    [SerializeField] private LayerMask m_groundLayer;
    [SerializeField] private LayerMask m_enemyLayer;

    [SerializeField] private Transform m_damagePosition;
    [SerializeField] private float m_damageRadius = 0.2f;

    private RangeAttackData m_data;
    private float m_xStartPosition;
    private bool m_isGravityOn;
    private bool m_hasStopped;
    private CircleCollider2D m_hitTrigger;

    private void Awake()
    {
        EnsureTriggerCollider();
    }

    private void Start()
    {
        if (m_data == null)
            return;

        m_rigidbody.gravityScale = 0f;
        m_rigidbody.linearVelocity = transform.right * m_data.speed;
        m_isGravityOn = false;
        m_xStartPosition = transform.position.x;
    }

    public void Initialize(RangeAttackData data)
    {
        if (m_data != null)
            return;
        m_data = data;
    }

    private void Update()
    {
        if (m_hasStopped || !m_isGravityOn)
            return;

        float angle = Mathf.Atan2(m_rigidbody.linearVelocity.y, m_rigidbody.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FixedUpdate()
    {
        if (m_hasStopped || m_data == null)
            return;

        if (!m_isGravityOn && Mathf.Abs(m_xStartPosition - transform.position.x) >= m_data.trevelDistance)
        {
            m_isGravityOn = true;
            m_rigidbody.gravityScale = m_gravity;
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
        if (m_hasStopped)
            return;

        int layer = other.gameObject.layer;

        if (IsLayerInMask(layer, m_enemyLayer) && TryGetDamageable(other, out var damageable))
        {
            damageable.TakeDamage(m_data.damage);
            Destroy(gameObject);
            return;
        }

        if (IsLayerInMask(layer, m_groundLayer))
        {
            StopProjectile();
        }
    }

    private void EnsureTriggerCollider()
    {
        if (!TryGetComponent(out m_hitTrigger))
            m_hitTrigger = gameObject.AddComponent<CircleCollider2D>();

        m_hitTrigger.isTrigger = true;
        m_hitTrigger.radius = m_damageRadius;

        Vector3 damagePos = m_damagePosition != null ? m_damagePosition.position : transform.position;
        m_hitTrigger.offset = (Vector2)transform.InverseTransformPoint(damagePos);
    }

    private static bool IsLayerInMask(int layer, LayerMask mask) =>
        (mask.value & (1 << layer)) != 0;

    private void StopProjectile()
    {
        m_hasStopped = true;
        m_rigidbody.gravityScale = 0f;
        m_rigidbody.linearVelocity = Vector2.zero;
    }

    private static bool TryGetDamageable(Component target, out IDamageable damageable)
    {
        if (target.TryGetComponent(out damageable))
            return true;
        damageable = target.GetComponentInParent<IDamageable>();
        return damageable != null;
    }
}
