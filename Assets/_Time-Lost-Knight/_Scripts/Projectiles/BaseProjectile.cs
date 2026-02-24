using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D m_rigidbody;
    [SerializeField] protected float m_gravity = 3f;

    [SerializeField] protected LayerMask m_groundLayer;
    [SerializeField] protected LayerMask m_enemyLayer;

    [SerializeField] protected float m_damageRadius = 0.2f;

    protected RangeAttackData m_data;
    protected float m_xStartPosition;
    protected bool m_isGravityOn;
    protected bool m_isFinished;
    protected CircleCollider2D m_hitTrigger;

    protected virtual void Awake()
    {
        EnsureTriggerCollider();
    }

    protected virtual void Start()
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

    protected virtual void Update()
    {
        if (m_isFinished || !m_isGravityOn)
            return;

        float angle = Mathf.Atan2(m_rigidbody.linearVelocity.y, m_rigidbody.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected virtual void FixedUpdate()
    {
        if (m_isFinished || m_data == null)
            return;

        if (!m_isGravityOn && Mathf.Abs(m_xStartPosition - transform.position.x) >= m_data.trevelDistance)
        {
            m_isGravityOn = true;
            m_rigidbody.gravityScale = m_gravity;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        HandleTrigger(other);
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        HandleTrigger(other);
    }

    private void HandleTrigger(Collider2D other)
    {
        if (m_isFinished)
            return;

        int layer = other.gameObject.layer;

        if (IsLayerInMask(layer, m_enemyLayer))
        {
            OnHitEnemy(other);
        }
        else if (IsLayerInMask(layer, m_groundLayer))
        {
            OnHitGround(other);
        }
    }

    protected virtual void OnHitEnemy(Collider2D other)
    {
        if (TryGetDamageable(other, out var damageable))
        {
            damageable.TakeDamage(m_data.damage);
            Finish();
        }
    }

    protected virtual void OnHitGround(Collider2D other)
    {
        StopProjectile();
    }

    protected virtual void EnsureTriggerCollider()
    {
        if (!TryGetComponent(out m_hitTrigger))
            m_hitTrigger = gameObject.AddComponent<CircleCollider2D>();

        m_hitTrigger.isTrigger = true;
        m_hitTrigger.radius = m_damageRadius;
        m_hitTrigger.offset = GetTriggerOffset();
    }

    protected virtual Vector2 GetTriggerOffset() => Vector2.zero;

    protected static bool IsLayerInMask(int layer, LayerMask mask) =>
        (mask.value & (1 << layer)) != 0;

    protected static bool TryGetDamageable(Component target, out IDamageable damageable)
    {
        if (target.TryGetComponent(out damageable))
            return true;
        damageable = target.GetComponentInParent<IDamageable>();
        return damageable != null;
    }
    protected virtual void StopProjectile()
    {
        m_isFinished = true;
        m_rigidbody.gravityScale = 0f;
        m_rigidbody.linearVelocity = Vector2.zero;
    }

    protected virtual void Finish()
    {
        Destroy(gameObject);
    }
}